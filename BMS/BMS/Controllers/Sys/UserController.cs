using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using BMS_Db.BLL.Sys.Sms;
using BMS_Db.BLL.Sys.User;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ys.Base.Tools.xTool;
using Ys.Tools;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Models;
using Ys.Tools.Response;

namespace BMS.Controllers.Sys
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly UserBaseBll _userBaseBll;
        private readonly BmsV1DbContext _dbContext;
        private readonly ILogger<UserController> _logger;
        private readonly SmsBll _smsBll;

        public UserController(UserBaseBll userBaseBll, ILogger<UserController> logger, BmsV1DbContext dbContext, SmsBll smsBll)
        {
            _userBaseBll = userBaseBll;
            _logger = logger;
            _dbContext = dbContext;
            _smsBll = smsBll;
        }
        [HttpPost]
        public ApiResult Add(User user)
        {

            _userBaseBll.Add(user);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }

        [HttpPost]
        public ApiResult Update(User user)
        {
            _userBaseBll.Edit(user);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }
        [HttpPost]
        public ApiResult Delete(User user)
        {
            _userBaseBll.Delete(user);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }


        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            var data = await _userBaseBll.GetUser();
            var pagination = new Pagination()
            {
                DefaultPageSize = 5,//默认多少条
                DefaultCurrent = 1,
                Total = data.Count
            };
            return ApiResult.True(new { data, pagination });
        }
        [HttpGet]
        public ApiResult GetEntityByCode(string code)
        {
            var data = _userBaseBll.GetUserEntityByCodeWithNoTracking(code);
            return ApiResult.True(new { data });
        }

        [HttpPost]
        public ApiResult BindPhone(JsonElement req)
        {
            var userCode = req.GetJsonString("Code").NotNull("用户编号为空");
            var logCode = req.GetJsonString("LogCode").NotNull("日志编号为空");
            var verifyCode = req.GetJsonString("VerifyCode").NotNull("验证码为空");
            var phone = req.GetJsonString("Phone").NotNull("手机号为空");
            var userEntityByCode = _userBaseBll.GetUserEntityByCode(userCode);
            var smsEntity = _smsBll.GetEntityByCode(logCode);
            var phoneExist = _userBaseBll.GetPhoneExist(phone);
            if (phoneExist)
            {
                return ApiResult.False("当前手机号已被绑定");
            }
            if (!string.IsNullOrEmpty(userEntityByCode.Phone))
            {
                return ApiResult.False("当前手机号已绑定");
            }
            if (smsEntity.ExpireDate.Minute >= DateTime.Now.Minute)
            {

                if (smsEntity.VerifyCode.Equals(verifyCode.Trim()))
                {
                    userEntityByCode.Phone = phone;
                    _dbContext.SaveChangesAsync();
                    return ApiResult.True();
                }
                else
                {
                    return ApiResult.False("验证码输入有误");
                }
            }
            else
            {
                return ApiResult.False("当前验证码过期");
            }
        }



        [HttpPost]
        public ApiResult EditPassword(JsonElement req)
        {
            var userCode = req.GetJsonString("Code").NotNull("用户编号为空");
            var password = req.GetJsonString("Password").NotNull("新密码为空");
            var oldPassword = req.GetJsonString("OldPassword").NotNull("新密码为空");
            var rePassword = req.GetJsonString("rePassword").NotNull("确认密码为空");
            var userEntityByCode = _userBaseBll.GetUserEntityByCode(userCode);
            var oldPasswordMd5 = Md5Tools.MD5_32(oldPassword + userEntityByCode.LoginPasswordSalt);
            var newPasswordMd5 = Md5Tools.MD5_32(rePassword + userEntityByCode.LoginPasswordSalt);

            if (oldPasswordMd5.Equals(userEntityByCode.LoginPassword))
            {
                userEntityByCode.LoginPassword = newPasswordMd5;
                _dbContext.SaveChangesAsync();
                return ApiResult.True("修改成功");
            }
            else
            {
                return ApiResult.False("旧密码输入错误");
            }
        }
    }
}
