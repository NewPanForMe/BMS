using System.Text.Json;
using BMS_Db.BLL.Sys.Module;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS.Controllers.Sys
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ModuleController : BaseController
    {

        private readonly BmsV1DbContext _dbContext;
        private readonly ModuleBll _moduleBll;
        public ModuleController(BmsV1DbContext dbContext, ModuleBll moduleBll)
        {
            _dbContext = dbContext;
            _moduleBll = moduleBll;
        }


        [HttpPost]
        public async Task<ApiResult> Add(Module module)
        {
            module.Code = Guid.NewGuid().ToString();
            module.CreateTime = DateTime.Now;
            _moduleBll.Add(module);
            await _dbContext.SaveChangesAsync();
            return ApiResult.True();
        }

        [HttpPost]
        public async Task<ApiResult> Update(Module module)
        {
            _moduleBll.Edit(module);
            await _dbContext.SaveChangesAsync();
            return ApiResult.True();
        }

        [HttpPost]
        public async Task<ApiResult> Delete(Module module)
        {
            _moduleBll.Delete(module);
            await _dbContext.SaveChangesAsync();
            return ApiResult.True();
        }

        [HttpGet]
        public async Task<ApiResult> GetList(string? treeCode)
        {
            var data = await _moduleBll.GetModules(treeCode ?? "");
            var pagination = new Pagination()
            {
                DefaultPageSize = 5,//默认多少条
                DefaultCurrent = 1,
                Total = data.Count
            };
            return ApiResult.True(new { data, pagination });
        }

        [HttpGet]
        public ApiResult GetEntityByCode(string code)
        {
            var data = _moduleBll.GetModuleEntityByCode(code);
            return ApiResult.True(new { data });
        }
        [HttpGet]
        public async Task<ApiResult> GetSelectOptions()
        {
            var data = await _moduleBll.GetSelectOptions();
            return ApiResult.True(new { data });
        }
        [HttpGet]
        public async Task<ApiResult> GetGroupSelectOptions()
        {
            var data = await _moduleBll.GetGroupSelectOptions();
            return ApiResult.True(new { data });
        }
    }
}
