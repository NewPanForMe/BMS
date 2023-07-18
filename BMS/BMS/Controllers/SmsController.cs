using System.Text.Json;
using BMS_Db.BLL.Module;
using BMS_Db.BLL.Sms;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using BMS_SMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Extra;
using Ys.Tools.MoreTool;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly SmsBll _smsBll;
        private readonly BmsV1DbContext _dbContext;
        public SmsController(SmsBll smsBll, BmsV1DbContext dbContext)
        {
            _smsBll = smsBll;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Authorize]
        public async Task<ApiResult> SendBindCode(JsonElement req)
        {
            var phone = req.GetJsonString("phone");
            phone=phone.NotNull("手机号为空，请重试");
            var generateSix = RandomTools.GenerateSix().ToString();
            var sendSmsCode = await _smsBll.SendSmsCode(phone, generateSix);
            var smsLog = new  SmsLog()
            {
                Code = Guid.NewGuid().ToString(),
                Phone = phone,
                VerifyCode = generateSix,
                SendTime = DateTime.Now,
                Type = "绑定",
                SmsResult = sendSmsCode.ToString(),
                ExpireDate = DateTime.Now.AddMinutes(5)
            };
            _dbContext.SmsLog.Add(smsLog);
            await _dbContext.SaveChangesAsync();
            return ApiResult.True(new { sendSmsCode.Message , smsLog.Code});
        }

        [HttpPost]
        public  async Task<ApiResult> SendRegisterCode(string phone)
        {
            phone.NotNull("手机号为空，请重试");
            var generateSix = RandomTools.GenerateSix().ToString();
            var sendSmsCode = await _smsBll.SendSmsCode(phone, generateSix);
            var smsLog = new SmsLog()
            {
                Code = Guid.NewGuid().ToString(),
                Phone = phone,
                VerifyCode = generateSix,
                SendTime = DateTime.Now,
                Type = "注册",
                SmsResult = sendSmsCode.ToString(),
                ExpireDate = DateTime.Now.AddMinutes(5)
            };
            _dbContext.SmsLog.Add(smsLog);
            await _dbContext.SaveChangesAsync();
            return ApiResult.True(new { sendSmsCode.Message , smsLog.Code});
        }



        [HttpGet]
        [Authorize]
        public async Task<ApiResult> GetList()
        {
            var data = await _smsBll.GetSmsLogs();
            var pagination = new Pagination()
            {
                DefaultPageSize = 5,//默认多少条
                DefaultCurrent = 1,
                Total = data.Count
            };
            return ApiResult.True(new { data, pagination });
        }
    }
}
