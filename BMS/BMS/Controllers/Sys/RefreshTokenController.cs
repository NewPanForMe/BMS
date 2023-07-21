
using System.Text.Json;
using BMS_Db.BLL.Sys.User;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Models;
using Ys.Tools.MoreTool;
using Ys.Tools.Response;

namespace BMS.Controllers.Sys
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RefreshTokenController : BaseController
    {

        private readonly UserBll _userBll;

        public RefreshTokenController(UserBll userBll)
        {
            _userBll = userBll;
        }


        [HttpPost]
        public async Task<ApiResult> RefreshToken(JsonElement req)
        {
            var jsonString = req.GetJsonString("token").NotNull("");
            var resolveToken = TokenTools.ResolveToken(jsonString);
            var expire = resolveToken.FirstOrDefault(x => x.Type == "ExpireTime")?.Value;
            var userCode = resolveToken.FirstOrDefault(x => x.Type == "UserCode")?.Value;
            var rExpireTime = DateTime.Parse(expire ?? DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            if (rExpireTime > DateTime.Now)
            {
                var generateToken = await _userBll.GenerateToken(userCode ?? "");
                return generateToken;
            }
            else if (rExpireTime < DateTime.Now)
            {
                HttpContext.Response.StatusCode = 401;
                return ApiResult.False();
            }
            else
            {
                return ApiResult.True();
            }
        }


    }
}
