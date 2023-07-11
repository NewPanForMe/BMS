﻿using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Tools.Interface;
namespace BMS_Db.BLL.Log;

public class LogBll : IBll
{
    private readonly BmsV1DbContext _dbContext;
    private readonly ILogger<LogBll> _logger;
    public LogBll(BmsV1DbContext dbContext, ILogger<LogBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// 获取日志
    /// </summary>
    /// <param name="value">值</param>
    /// <returns></returns>
    public async Task<List<NLog>> GetLogs(int count)
    {
        var listAsync = await _dbContext.NLog.OrderByDescending(x=>x.Logged).Take(count).ToListAsync();
        _logger.LogWarning("获取日志列表：{newList}", listAsync.Count);
        return listAsync;
    }


}