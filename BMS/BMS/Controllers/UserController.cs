using System.Text.Json;
using BMS_Db.BLL.Module;
using BMS_Db.BLL.User;
using BMS_Db.EfContext;
using BMS_Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        public ApiResult Update(User user)
        {

            _userBaseBll.Edit(user);
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
            return ApiResult.True();
        }
        [HttpPost]
        public ApiResult Delete(User user)
        {
            _userBaseBll.Delete(user);
            _dbContext.SaveChanges();
            return ApiResult.True();
        }


        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            var data = await _userBaseBll.GetUser();
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
            var data = _userBaseBll.GetUserEntityByCode(code);
            return ApiResult.True(new { data });
        }
    }
}
