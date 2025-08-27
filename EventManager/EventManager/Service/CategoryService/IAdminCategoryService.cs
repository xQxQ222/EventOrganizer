using ModelHolder.Dto;
using ModelHolder.Models;
using System.Threading.Tasks;

namespace EventManager.Service.CategoryService
{
    public interface IAdminCategoryService
    {
        Task<Category> CreateCategory(long userId, CategoryDto categoryDto);

        Task<Category> UpdateCategory(long userId, long categoryId, CategoryDto categoryDto);

        Task DeleteCategory(long userId, long categoryId);
    }
}
