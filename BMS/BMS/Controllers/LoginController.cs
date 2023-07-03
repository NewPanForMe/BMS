using BMS_Db.BLL;
using BMS_Db.EfContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BmsV1DbContext _dbContext;
        private readonly UserBll _userBll;
        private readonly ILogger<LoginController> _logger;

        public LoginController( UserBll userBll, ILogger<LoginController> logger, BmsV1DbContext dbContext)
        {
            _userBll = userBll;
            _logger = logger;
            _dbContext = dbContext;
        }
        [HttpPost]
        public ApiResult Check(JsonElement req)
        {

            var userName = req.GetJsonString("userName").HasValueNoNameOrPwd("用户名为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            _logger.LogInformation($"{userName}登录");
            return _userBll.Check(userName, password); ;
        }
        [HttpGet]
        public void Test()
        {
            _logger.LogWarning("发生了事情");
        }

    }
}
