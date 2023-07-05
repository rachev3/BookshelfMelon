using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IUpvoteService
    {
        Task<IEnumerable<Upvote>> GetAll();
        Task<Upvote> GetById(int id);
        Task Add(Upvote upvote);
        Task<Upvote> Update(int id, Upvote upvote);
        Task Delete(int id);
    }
}
