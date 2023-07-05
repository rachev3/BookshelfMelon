using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task<Category> Update(int id, Category category);
        Task Delete(int id);
    }
}
