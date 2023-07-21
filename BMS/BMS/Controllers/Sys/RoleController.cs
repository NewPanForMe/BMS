using BMS_Db.BLL.Sys.Role;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Consul;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Response;
using Role = BMS_Models.DbModels.Role;

namespace BMS.Controllers.Sys
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly RoleBll _roleBll;
        private readonly BmsV1DbContext _dbContext;
        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleBll roleBll, BmsV1DbContext dbContext, ILogger<RoleController> logger)
        {
            _roleBll = roleBll;
            _dbContext = dbContext;
            _logger = logger;
        }
        [HttpPost]
        public ApiResult Add(Role role)
        {
            _roleBll.Add(role);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }

        [HttpPost]
        public ApiResult Update(Role role)
        {
            _roleBll.Edit(role);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }
        [HttpPost]
        public ApiResult Delete(Role role)
        {
            _roleBll.Delete(role);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }
        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            var data = await _roleBll.GetRole();
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
            var data = _roleBll.GetRoleEntityByCode(code);
            return ApiResult.True(new { data });
        }

        [HttpGet]
        public async Task<ApiResult> GetRoleOptions()
        {
            var data = await _roleBll.GetSelectOptions();
            return ApiResult.True(new { data });
        }
    }
}
