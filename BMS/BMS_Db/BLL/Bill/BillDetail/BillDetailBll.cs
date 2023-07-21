using BMS_Db.BLL.Sys.Sms;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.Models;
using Ys.Tools.Response;

namespace BMS_Db.BLL.Bill.BillDetail;

public class BillDetailBll : IBll
{
    private readonly BmsV1DbContext _dbContext;
    private readonly ILogger<BillDetailBll> _logger;

    public BillDetailBll(BmsV1DbContext dbContext, ILogger<BillDetailBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public List<BMS_Models.DbModels.BillDetail> GetBillDetails(string billCode)
    {
        var listAsync = _dbContext.BillDetail.AsNoTracking().OrderByDescending(x => x.Date).ToList();

        if (!string.IsNullOrEmpty(billCode))
        {
            listAsync = listAsync.Where(x => x.BillCode == billCode).ToList();
        }
        return listAsync;
    }
    /// <summary>
    /// 获取单条
    /// </summary>
    /// <returns></returns>
    public BMS_Models.DbModels.BillDetail GetBillDetailEntityByCode(string code)
    {
        code.NotNull("传入编号为空");
        var bill = _dbContext.BillDetail.FirstOrDefault(x => x.Code.Equals(code)).NotNull("当前数据不存在");
        return bill;
    }

    /// <summary>
    /// 获取单条
    /// </summary>
    /// <returns></returns>
    public async Task<ApiResult> GetTypeChart(CurrentUser current)
    {
        var list = from detail in _dbContext.BillDetail
                   join billx in _dbContext.Bill on detail.BillCode equals billx.Code
                   where billx.UserCode.Equals(current.Code)
                   select new
                   {
                       detail.Type,
                       detail.Money
                   };
        var bill = await list.GroupBy(x => x.Type)
                        .Select(x =>
                            new
                            {
                                Value = x.Sum(y => y.Money),
                                Name = x.Key,
                            }).ToListAsync(); ;
        return ApiResult.True(new { data = bill });
    }
}