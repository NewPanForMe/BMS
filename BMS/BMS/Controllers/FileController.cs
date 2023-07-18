using System.Text.Json;
using BMS_Base.Config;
using BMS_Db.BLL.File;
using BMS_Db.BLL.Sms;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FileController : BaseController
    {
        private readonly BmsV1DbContext _dbContext;
        private readonly FileBll _fileBll;
        public FileController(BmsV1DbContext dbContext, FileBll fileBll)
        {
            _dbContext = dbContext;
            _fileBll = fileBll;
        }


        [HttpPost]
        public async Task<ApiResult> Upload(IFormFile file)
        {
            var upload =await _fileBll.Upload(file, CurrentUser.Code, CurrentUser.Name);
            await _dbContext.SaveChangesAsync();
            return ApiResult.True(new { code= upload });
        }


        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            var data = await _fileBll.GetFileUpload();
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
