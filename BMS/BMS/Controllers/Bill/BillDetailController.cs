using BMS_Db.BLL.Bill.Bill;
using BMS_Db.BLL.Bill.BillDetail;
using BMS_Db.BLL.Sys.File;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Response;

namespace BMS.Controllers.Bill
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BillDetailController : ControllerBase
    {

        private readonly BmsV1DbContext _dbContext;
        private readonly BillDetailBll _billDetailBll;
        public BillDetailController(BmsV1DbContext dbContext, BillDetailBll billDetailBll)
        {
            _dbContext = dbContext;
            _billDetailBll = billDetailBll;
        }


        [HttpGet]
        public  ApiResult GetList(string? billCode)
        {
            var data =  _billDetailBll.GetBillDetails(billCode ?? "");
            var pagination = new Pagination()
            {
                DefaultPageSize = 5,//默认多少条
                DefaultCurrent = 1,
                Total = data.Count
            };
            return ApiResult.True(new { data, pagination });
        }

        [HttpGet]
        public ApiResult GetBillDetailEntityByCode(string code)
        {
            var data = _billDetailBll.GetBillDetailEntityByCode(code);
            return ApiResult.True(new { data });
        }


    }
}
