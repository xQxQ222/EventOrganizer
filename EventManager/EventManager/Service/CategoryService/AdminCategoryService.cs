using AutoMapper;
using EventManager.Utility;
using Microsoft.EntityFrameworkCore;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Exceptions;
using ModelHolder.Models;
using System.Threading.Tasks;

namespace EventManager.Service.CategoryService
{
    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;
        private readonly HelperMethods helperMethods;

        public AdminCategoryService(EventManagerDbContext dbContext, IMapper mapper, HelperMethods helperMethods)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.helperMethods = helperMethods;
        }
        public async Task<Category> CreateCategory(long userId, CategoryDto categoryDto)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            var category = mapper.Map<Category>(categoryDto);
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(long userId, long categoryId)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            var category = dbContext.Categories.Find(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"Категория с id {categoryId} не найдена");
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Category> UpdateCategory(long userId, long categoryId, CategoryDto categoryDto)
        {
            helperMethods.VerifyUserExistence(userId);
            helperMethods.CheckAdminRights(userId);
            var category = await dbContext.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"Категория с id {categoryId} не найдена");
            }
            if (categoryDto.CategoryName != null)
            {
                category.CategoryName = categoryDto.CategoryName;
            }
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
