using EventManager.Utility;
using ModelHolder.Context;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Service.CategoryService
{
    public class UserCategoryService : IUserCategoryService
    {

        private readonly EventManagerDbContext dbContext;
        private readonly HelperMethods helperMethods;

        public UserCategoryService(EventManagerDbContext dbContext, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.helperMethods = helperMethods;
        }

        public async Task<List<Category>> GetAllCategories(long userId)
        {
            helperMethods.VerifyUserExistence(userId);
            var categories = dbContext.Categories.ToList();
            return categories;
        }

        public async Task<Category> GetCategoryById(long userId, long id)
        {
            helperMethods.VerifyUserExistence(userId);
            var category = dbContext.Categories.FirstOrDefault(x=>x.CategoryId == id);

            if (category == null)
            {
                throw new NotFoundException($"Категория с id {id} не найдена");
            }

            return category;
        }
    }
}
