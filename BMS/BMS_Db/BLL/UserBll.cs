using System.Security.Claims;
using BMS_Base.Interface;
using BMS_Db.EfContext;
using Consul;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Ys.Base.Tools.xTool;
using Ys.Tools.Extra;
using Ys.Tools.MoreTool;
using Ys.Tools.Response;

namespace BMS_Db.BLL;

public class UserBll:IBll
{
    private readonly BmsV1DbContext _dbContext;
    private readonly ILogger<UserBaseBll> _logger;
    public UserBll(BmsV1DbContext dbContext, ILogger<UserBaseBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// 登录校验
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    public string Check(string userName, string password)
    {
        var user = _dbContext.User.FirstOrDefault(x => x.LoginName == userName).NotNull("用户不存在");
        var passSecret = Md5Tools.MD5_32(password).ToLower();
        user?.IsDelete.IsBool("账户已删除");
        user?.IsLock.IsBool("账户已锁定");
        if (user != null && user.LoginPassword.Equals(passSecret))
        {
            user.JwtVersion++;
            var listClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Name ?? ""),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Version,user.JwtVersion.ToString()),
                new Claim("UserCode",user.Code),
            };
            var token = TokenTools.Create(listClaims);
            return token;
        }
        return "账户密码不正确";
    }

}