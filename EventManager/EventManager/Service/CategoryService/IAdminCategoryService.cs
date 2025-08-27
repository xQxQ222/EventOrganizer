using ModelHolder.Dto;
using ModelHolder.Models;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Service.CategoryService
{
    public interface IAdminCategoryService
    {
        Task<Category> CreateCategory(CategoryDto categoryDto);

        Task<Category> UpdateCategory(long categoryId, CategoryDto categoryDto);

        Task DeleteCategory(long categoryId);
    }
}
