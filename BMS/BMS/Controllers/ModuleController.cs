using BMS_Db.BLL.Module;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ModuleController : ControllerBase
    {

        private readonly BmsV1DbContext _dbContext;
        private readonly ModuleBll _moduleBll;
        private readonly ILogger<ModuleController> _logger;

        public ModuleController(BmsV1DbContext dbContext, ILogger<ModuleController> logger, ModuleBll moduleBll)
        {
            _dbContext = dbContext;
            _logger = logger;
            _moduleBll = moduleBll;
        }


        [HttpPost]
        public async Task<ApiResult> Add(Module module)
        {
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
        public async Task<ApiResult> GetList()
        {
            return await _moduleBll.GetModules();
        }
    }
}
