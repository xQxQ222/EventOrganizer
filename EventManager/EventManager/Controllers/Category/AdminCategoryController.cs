using EventManager.Service.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Threading.Tasks;

namespace EventManager.Controllers.CategoryService
{
    [ApiController]
    [Route("/api/admin/categories")]
    public class AdminCategoryController : Controller
    {
        private readonly ILogger<AdminCategoryController> log;
        private readonly IAdminCategoryService adminCategoryService;

        public AdminCategoryController(ILogger<AdminCategoryController> logger, IAdminCategoryService adminCategoryService)
        {
            log = logger;
            this.adminCategoryService = adminCategoryService;
        }

        [HttpPost]
        public async Task<Category> CreateNewCategory([FromHeader(Name = "X-User-Id")] long userId, CategoryDto categoryDto)
        {
            log.LogInformation("POST /api/admin/categories с телом: {}", categoryDto);
            return await adminCategoryService.CreateCategory(userId, categoryDto);
        }

        [HttpPatch]
        public async Task<Category> UpdateCategory([FromHeader(Name = "X-User-Id")] long userId, long categoryId, CategoryDto categoryDto)
        {
            log.LogInformation("PATCH /api/admin/categories с параметрами categoryId: {}, categoryDto: {}", categoryId, categoryDto);
            return await adminCategoryService.UpdateCategory(userId, categoryId, categoryDto);
        }

        [HttpDelete]
        public async void DeleteCategory([FromHeader(Name = "X-User-Id")] long userId, long categoryId)
        {
            log.LogInformation("DELETE /api/admin/categories. categoryId: {} ", categoryId);
            await adminCategoryService.DeleteCategory(userId, categoryId);
        }
    }
}
