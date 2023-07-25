using BMS_Db.BLL.Bill.BillDetail;
using BMS_Db.EfContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Response;

namespace BMS.Controllers.Bill
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ChartController : BaseController
    {
        private readonly BmsV1DbContext _dbContext;
        private readonly BillDetailBll _billDetailBll;
        public ChartController(BmsV1DbContext dbContext, BillDetailBll billDetailBll)
        {
            _dbContext = dbContext;
            _billDetailBll = billDetailBll;
        }

        [HttpGet]
        public async Task<ApiResult> GetTypeChart(int year, int month,string type)
        {
            var typeChart =await _billDetailBll.GetTypeChart(CurrentUser, year,month, type);
            return typeChart;
        }


        [HttpGet]
        public async Task<ApiResult> GetPayTypeChart(int year,int month)
        {
            var typeChart = await _billDetailBll.GetPayTypeChart(CurrentUser, year,month);
            return typeChart;
        }

        [HttpGet]
        public async Task<ApiResult> GetInChart(int year, int month, string type)
        {
            var typeChart = await _billDetailBll.GetInChart(CurrentUser, year, month, type);
            return typeChart;
        }
    }
}
