using BMS_Db.BLL.Module;
using BMS_Db.EfContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Models;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuController : BaseController
    {
        private readonly BmsV1DbContext _dbContext;
        private readonly ModuleBll _moduleBll;
        public MenuController(BmsV1DbContext dbContext, ModuleBll moduleBll)
        {
            _dbContext = dbContext;
            _moduleBll = moduleBll;
        }

        #region 菜单
        [HttpGet]
        public async Task<ApiResult> Menu()
        {
            var menuNode = await _moduleBll.GetMenuNode(CurrentUser?.ListRole??new string[]{});
            await _dbContext.SaveChangesAsync();
            return ApiResult.True(new { menuNode });
        }

        #endregion



    }
}
