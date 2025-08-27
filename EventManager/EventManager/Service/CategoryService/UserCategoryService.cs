using AutoMapper;
using EventManager.Exceptions;
using ModelHolder.Context;
using ModelHolder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Service.CategoryService
{
    public class UserCategoryService : IUserCategoryService
    {

        private readonly EventManagerDbContext dbContext;

        public UserCategoryService(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = dbContext.Categories.ToList();
            return categories;
        }

        public async Task<Category> GetCategoryById(long id)
        {
            var category = dbContext.Categories.FirstOrDefault(x=>x.CategoryId == id);

            if (category == null)
            {
                throw new NotFoundException($"Категория с id {id} не найдена");
            }

            return category;
        }
    }
}
