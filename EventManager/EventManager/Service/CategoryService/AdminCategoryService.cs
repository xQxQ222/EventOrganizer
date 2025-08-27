using AutoMapper;
using EventManager.Exceptions;
using ModelHolder.Context;
using ModelHolder.Dto;
using ModelHolder.Models;
using System.Threading.Tasks;
using UtilityHolder.Dto;

namespace EventManager.Service.CategoryService
{
    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly EventManagerDbContext dbContext;
        private readonly IMapper mapper;

        public AdminCategoryService(EventManagerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Category> CreateCategory(CategoryDto categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(long categoryId)
        {
            var category = await dbContext.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"Категория с id {categoryId} не найдена");
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Category> UpdateCategory(long categoryId, CategoryDto categoryDto)
        {
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
