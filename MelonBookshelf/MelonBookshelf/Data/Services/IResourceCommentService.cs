using MelonBookshelf.Data.DTO;
using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IResourceCommentService
    {
        Task<List<ResourceComment>> GetAll();
        Task<ResourceComment> GetById(int id);
        Task Add(ResourceComment category);
        Task<ResourceComment> Update(ResourceComment category);
        Task Delete(int id);
    }
}

