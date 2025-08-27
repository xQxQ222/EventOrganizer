using ModelHolder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Service.CategoryService
{
    public interface IUserCategoryService
    {
        Task<Category> GetCategoryById(long userId, long id);

        Task<List<Category>> GetAllCategories(long userId);
    }
}
