using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IResourceService
    {
        Task<IEnumerable<Resource>> GetAll();
        Task<Resource> GetById(int id);
        Task Add(Resource resource);
        Task<Resource> Update(int id, Resource resource);
        Task Delete(int id);
    }
}