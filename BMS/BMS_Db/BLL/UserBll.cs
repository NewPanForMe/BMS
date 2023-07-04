using System.Security.Claims;
using BMS_Base.Config;
using BMS_Db.EfContext;
using Microsoft.Extensions.Logging;
using Ys.Base.Tools.xTool;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.MoreTool;
using Ys.Tools.Response;

namespace BMS_Db.BLL;

public  class UserBll : IBll
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
    public ApiResult Check(string userName, string password)
    {
        var user = _dbContext.User.FirstOrDefault(x => x.LoginName == userName).NotNull($"用户{userName}不存在");
        var passSecret = Md5Tools.MD5_32(password + user.LoginPasswordSalt).ToLower();
        Console.WriteLine($"passSecret={passSecret}");
        user.IsDelete.IsBool($"账户{userName}已删除");
        user.IsLock.IsBool($"账户{userName}已锁定");
        if (!user.LoginPassword.Equals(passSecret))
        {
            user.ErrorCount += 1;
            if (user.ErrorCount == 3)
            {
                user.IsLock=true;
                user.ErrorCancelTime = DateTime.Now.AddMinutes(SystemConfig.Instance.ErrorCount);
                _dbContext.SaveChangesAsync();
                _logger.LogWarning("{userName}账户锁定，锁定时长：{ SystemConfig.Instance.ErrorCount}分钟", userName, SystemConfig.Instance.ErrorCount);
                return ApiResult.False("您的账户已锁定，请于{SystemConfig.Instance.ErrorCount}分钟后重试", SystemConfig.Instance.ErrorCount);
            }
            _dbContext.SaveChangesAsync();
            _logger.LogWarning("{userName}账户密码不正确", userName);
            return ApiResult.False("账户密码不正确");
        }
        user.JwtVersion++;
        user.ErrorCount=0;
        var listClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.Name ?? ""),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Version,user.JwtVersion.ToString()),
            new Claim("UserCode",user.Code),
        };
        var token = TokenTools.Create(listClaims);
        _logger.LogWarning("{userName}登录成功，生成token【{token}】", userName, token);
        return ApiResult.True(new { token });
    }

}