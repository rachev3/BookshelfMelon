using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IFollowerService
    { Task<List<Follower>> GetAll();
        Task<Follower> GetById(int id);
        Task Add(Follower follower);
        Task<Follower> Update(int id, Follower follower);
        Task Delete(int id);
    }
}
