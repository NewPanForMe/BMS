using BMS_Db.BLL.User;
using BMS_Db.EfContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Tools.Interface;
using Ys.Tools.Response;
using BMS_Models.DbModels;
namespace BMS_Db.BLL.Module;

public class ModuleBll:IBll
{
    private readonly BmsV1DbContext _dbContext;
    private readonly ILogger<ModuleBll> _logger;
    public ModuleBll(BmsV1DbContext dbContext, ILogger<ModuleBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    /// <summary>
    /// 新增模块
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    public void Add(BMS_Models.DbModels.Module module)
    {
        _dbContext.Module.Add(module);
        _logger.LogWarning("新增模块：{module}", module);
    }

    /// <summary>
    /// 修改模块
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    public void Edit(BMS_Models.DbModels.Module module)
    {
        _dbContext.Module.Update(module);
        _logger.LogWarning("修改模块：{module}", module);

    }

    /// <summary>
    /// 删除模块
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    public void Delete(BMS_Models.DbModels.Module module)
    {

        _dbContext.Module.Remove(module);
        _logger.LogWarning("删除模块：{module}", module);

    }

    /// <summary>
    /// 获取模块
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult> GetModules()
    {
        var listAsync = await _dbContext.Module.ToListAsync();
        _logger.LogWarning("获取用户列表：{user}", listAsync.Count);
        return ApiResult.True(listAsync);
    }

}