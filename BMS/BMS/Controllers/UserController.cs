using System.Text.Json;
using BMS_Db.BLL;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly  UserBaseBll _userBaseBll;
        private readonly  BmsV1DbContext _dbContext;
        private readonly  UserBll _userBll;
        private readonly  ILogger<UserController> _logger;

        public UserController(UserBaseBll userBaseBll, UserBll userBll, ILogger<UserController> logger, BmsV1DbContext dbContext)
        {
            _userBaseBll = userBaseBll;
            _userBll = userBll;
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpPost]
        [Authorize]
        public ApiResult Add(User user)
        {
            _userBaseBll.Add(user);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }

        [HttpPost]
        public ApiResult Check(JsonElement req)
        {
            var userName = req.GetJsonString("userName").HasValueNoNameOrPwd("用户名为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            var token = _userBll.Check(userName, password);
            return ApiResult.True(token);
        }
    }
}
