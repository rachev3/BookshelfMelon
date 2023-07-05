using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MelonBookshelf.Data.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ApplicationDbContext _appDbContext;

        public CategoryService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Category category)
        {
            await _appDbContext.Categories.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var result = await _appDbContext.Categories.FirstOrDefaultAsync(n => n.CategoryId == id);
            _appDbContext.Categories.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var result = await _appDbContext.Categories.ToListAsync();
            return result;
        }

        public async Task<Category> GetById(int id)
        {
            var result = await _appDbContext.Categories.FirstOrDefaultAsync(n => n.CategoryId == id);
            return result;
        }

        public async Task<Category> Update(int id, Category category)
        {
            _appDbContext.Update(category);
            await _appDbContext.SaveChangesAsync();
            return category;
        }

    }
}
