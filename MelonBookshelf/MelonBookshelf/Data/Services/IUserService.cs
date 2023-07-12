using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByName(string name);
        Task Add(User user, string password);
        Task<User> Update(string id, User user);
        Task Delete(string id);
    }
}
