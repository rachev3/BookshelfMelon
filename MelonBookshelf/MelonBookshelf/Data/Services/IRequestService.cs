
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
        Task<List<string>> GetFollowersEmails(int requestId);
        Task Add(Request request); 
        Task Like(int requestId, string userId);
        Task Dislike(int requestId, string userId);
        Task Follow(int requestId, string userId);
        Task UnFollow(int requestId, string userId);
        Task<Request> Update( Request request);
        Task Delete(int id);
    }
}
