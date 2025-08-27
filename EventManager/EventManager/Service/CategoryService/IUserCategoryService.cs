using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Service.CategoryService
{
    public interface IUserCategoryService
    {
        Task<Category> GetCategoryById(long id);

        Task<List<Category>> GetAllCategories();
    }
}
