using MelonBookshelf.Data.DTO;
using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IResourceService
    {
        Task<List<Resource>> GetAll();
        Task<Resource> GetById(int id);
       
        Task Add(Resource resource);
        Task AddDownload(ResourceDownloadHistory resourceDownloadHistory);
        Task<Resource> Update(int id, Resource resource);
        Task<List<Resource>> Search(string? title,ResourceType? type, int? categoryId);
        Task Delete(int id);
        Task Want(string userId, int resourceId);
        Task Unwant(string userId, int resourceId);
    }
} 