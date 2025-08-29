using ModelHolder.Dto;
using ModelHolder.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramUI.RefitClient.CategoryClient
{
    public interface ICategoryApi
    {
        [Post("/api/admin/categories")]
        Task<Category> CreateNewCategory([Header("X-User-Id")] long userId, [Body] CategoryDto categoryDto);

        [Patch("/api/admin/categories/{categoryId}")]
        Task<Category> UpdateCategory([Header("X-User-Id")] long userId, int categoryId, [Body] CategoryDto categoryDto);

        [Delete("/api/admin/categories/{categoryId}")]
        Task DeleteCategory([Header("X-User-Id")] long userId, long categoryId);

        [Get("/api/categories/{categoryId}")]
        Task<Category> GetCategoryById([Header("X-User-Id")] long userId, long categoryId);

        [Get("/api/categories")]
        Task<List<Category>> GetAllCategories([Header("X-User-Id")] long userId);
    }
}
