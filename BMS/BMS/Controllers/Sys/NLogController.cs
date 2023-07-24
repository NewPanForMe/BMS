using BMS_Db.BLL.Sys.NLog;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Ys.Tools.Controllers;
using Ys.Tools.Response;

namespace BMS.Controllers.Sys
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class NLogController : BaseController
    {

        private readonly BmsV1DbContext _dbContext;
        private readonly LogBll _logBll;
        public NLogController(BmsV1DbContext dbContext, LogBll logBll)
        {
            _dbContext = dbContext;
            _logBll = logBll;
        }

        [HttpGet]
        public async Task<ApiResult> GetLogList()
        {
            var data = await _logBll.GetLogs(50);
            var pagination = new Pagination()
            {
                DefaultPageSize = 5,//默认多少条
                DefaultCurrent = 1,
                Total = data.Count
            };
            return ApiResult.True(new { data, pagination });
        }
    }
}
