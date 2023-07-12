
using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IRequestService
    {
        Task<List<Request>> GetAll();
        Task<Request> GetById(int id);
        Task<List<Request>> GetMyRequests(string userId);
        Task<List<Request>> GetFollowingRequests(string userId);
        Task<List<Request>> GetPendingRequests();
        Task Add(Request request); 
        Task Like(int requestId, string userId);
        Task Dislike(int requestId, string userId);
        Task Follow(int requestId, string userId);
        Task UnFollow(int requestId, string userId);
        Task<int> GetUpvotersCount(int requestId);
        Task<int> GetFollowersCount(int requestId);
        Task<Request> Update(int id, Request request);
        Task Delete(int id);
    }
}
