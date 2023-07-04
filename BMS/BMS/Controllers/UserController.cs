using System.Text.Json;
using BMS_Db.BLL;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Models;
using Ys.Tools.Response;

namespace BMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly  UserBaseBll _userBaseBll;
        private readonly  BmsV1DbContext _dbContext;
        private readonly  ILogger<UserController> _logger;

        public UserController(UserBaseBll userBaseBll,  ILogger<UserController> logger, BmsV1DbContext dbContext)
        {
            _userBaseBll = userBaseBll;
            _logger = logger;
            _dbContext = dbContext;
        }
        [HttpPost]
        public ApiResult Add(User user)
        {
            
            _userBaseBll.Add(user);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }

        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            var user = await _userBaseBll.GetUser();
            return user;
        }

        [HttpGet]
        public ApiResult GetUserTokenInfo()
        {
            return ApiResult.True(CurrentUser.ToString());
        }
    }
}
