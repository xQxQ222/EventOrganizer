using EventManager.Controllers.CategoryService;
using EventManager.Service.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Controllers.CategoryService
{
    [ApiController]
    [Route("/api/categories")]
    public class UserCategoryController : Controller
    {

        private readonly ILogger<UserCategoryController> log;
        private readonly IUserCategoryService userCategoryService;

        public UserCategoryController(ILogger<UserCategoryController> log, IUserCategoryService userCategoryService)
        {
            this.log = log;
            this.userCategoryService = userCategoryService;
        }

        [HttpGet("{id:long}")]
        public async Task<Category> GetCategoryById([FromHeader(Name = "X-User-Id")] long userId, long id)
        {
            log.LogInformation("GET /api/categories/{}", id);
            return await userCategoryService.GetCategoryById(userId, id);
        }

        [HttpGet]
        public async Task<List<Category>> GetAllCategories([FromHeader(Name = "X-User-Id")] long userId)
        {
            log.LogInformation("GET /api/categories");
            return await userCategoryService.GetAllCategories(userId);
        }
    }
}
