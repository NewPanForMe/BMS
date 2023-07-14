using BMS_Db.BLL.Sms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.MoreTool;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly SmsBll _smsBll;

        public SmsController(SmsBll smsBll)
        {
            _smsBll = smsBll;
        }


        /// <summary>
        /// 
        /// </summary>
        ///
        [HttpGet]
        public  void SendBindCode(string phone)
        {
            var generateSix = RandomTools.GenerateSix();
             _smsBll.SendSmsCode(phone, generateSix.ToString());
        }
    }
}
