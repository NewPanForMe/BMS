using BMS_Db.BLL.Module;
using BMS_Db.EfContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Response;

namespace BMS.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class TreeController : BaseController
{
    private readonly BmsV1DbContext _dbContext;
    private readonly ModuleBll _moduleBll;
    public TreeController(BmsV1DbContext dbContext, ModuleBll moduleBll)
    {
        _dbContext = dbContext;
        _moduleBll = moduleBll;
    }
    #region Module
    [HttpGet]
    public async Task<ApiResult> ModuleTree()
    {
        var treeNode =await _moduleBll.GetTreeNode();
        await _dbContext.SaveChangesAsync();
        return ApiResult.True(new { treeNode });
    }

    #endregion




}