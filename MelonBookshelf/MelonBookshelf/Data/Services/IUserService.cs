using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task Add(User user);
        Task<User> Update(string id, User user);
        Task Delete(string id);
    }
}
