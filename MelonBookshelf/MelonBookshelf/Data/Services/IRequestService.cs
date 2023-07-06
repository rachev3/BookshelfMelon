
using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IRequestService
    {
        Task<List<Request>> GetAll();
        Task<Request> GetById(int id);
        Task Add(Request request);
        Task<Request> Update(int id, Request request);
        Task Delete(int id);
    }
}
