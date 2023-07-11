using BMS_Db.EfContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Base.Tools.xTool;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.Response;
namespace BMS_Db.BLL.User;
using BMS_Models.DbModels;
/// <summary>
/// User基础操作类-增删改查
/// </summary>
public class UserBaseBll : IBll
{
    private readonly BmsV1DbContext _dbContext;
    private readonly ILogger<UserBaseBll> _logger;
    public UserBaseBll(BmsV1DbContext dbContext, ILogger<UserBaseBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public void Add(User user)
    {
        user.LoginPasswordSalt = Guid.NewGuid().ToString();
        user.LoginPassword = Md5Tools.MD5_32(user.LoginPassword + user.LoginPasswordSalt);
        user.JwtVersion = 1;
        user.Code= Guid.NewGuid().ToString();
        user.IsLock = false;
        user.ErrorCount = 0;
        user.ErrorCancelTime= DateTime.Now;
        _dbContext.User.Add(user);
        _logger.LogWarning("新增用户：{user}", user);
    }

    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public void Edit(User user)
    {
        var userEntityByCode = GetUserEntityByCode(user.Code);
        if (string.IsNullOrEmpty(user.LoginPassword))
        {
            user.LoginPassword = userEntityByCode.LoginPassword;
        }
        else
        {
            user.LoginPassword = Md5Tools.MD5_32(user.LoginPassword + userEntityByCode.LoginPasswordSalt);
        }

        user.ErrorCount = userEntityByCode.ErrorCount;
        user.ErrorCancelTime = userEntityByCode.ErrorCancelTime;
        user.LoginPasswordSalt = userEntityByCode.LoginPasswordSalt;
        _dbContext.User.Update(user);
        _logger.LogWarning("修改用户：{user}", user);

    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public void Delete(User user)
    {
        _dbContext.User.Remove(user);
        _logger.LogWarning("删除用户：{user}", user);

    }

    /// <summary>
    /// 获取用户
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetUser()
    {
        var listAsync = await _dbContext.User.AsNoTracking().ToListAsync();
        _logger.LogWarning("获取用户列表：{user}", listAsync.Count);
        return (listAsync);
    }




    /// <summary>
    /// 获取模块
    /// </summary>
    /// <returns></returns>
    public User GetUserEntityByCode(string code)
    {
        code.NotNull("传入编号为空");
        var user = _dbContext.User.AsNoTracking().FirstOrDefault(x => x.Code.Equals(code));
        user = user.NotNull("当前数据不存在");
        _logger.LogWarning("获取用户{code}：{user}", code, user);
        return user;
    }

}