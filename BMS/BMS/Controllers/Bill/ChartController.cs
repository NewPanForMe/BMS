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
        public async Task<ApiResult> GetTypeChart()
        {
            var typeChart =await _billDetailBll.GetTypeChart(CurrentUser);
            return typeChart;
        }







    }
}
