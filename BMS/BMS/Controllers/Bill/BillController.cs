using System.Text.Json;
using BMS_Db.BLL.Bill.Bill;
using BMS_Db.BLL.Bill.BillDetail;
using BMS_Db.BLL.Sys.File;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS.Controllers.Bill
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BillController : BaseController
    {
        private readonly BmsV1DbContext _dbContext;
        private readonly BillBll _billBll;
        public BillController(BmsV1DbContext dbContext, BillBll billBll)
        {
            _dbContext = dbContext;
            _billBll = billBll;
        }


        /// <summary>
        /// 导入数据，并保存
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResult> BillUpload(JsonElement req)
        {
            var year = req.GetJsonNum("Year");
            var month = req.GetJsonNum("Month");
            var type = req.GetJsonString("Type").NotNull("类型为空");
            var path = req.GetJsonString("Location").NotNull("路径为空");
            var billAdd = await _billBll.BillAdd(year, month, path, type, CurrentUser);
            await _dbContext.SaveChangesAsync();
            return billAdd;
        }

        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            var data = await _billBll.GetBills(CurrentUser.Code);
            var pagination = new Pagination()   
            {
                DefaultPageSize = 5,//默认多少条
                DefaultCurrent = 1,
                Total = data.Count
            };
            return ApiResult.True(new { data, pagination });
        }

        [HttpGet]
        public ApiResult GetBillEntityByCode(string code)
        {
            var data = _billBll.GetBillEntityByCode(code);
            return ApiResult.True(new { data });
        }


    }
}
