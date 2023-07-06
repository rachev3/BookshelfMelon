using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IWantedResourcesService
    {
        Task<List<WantedResources>> GetAll();
        Task<WantedResources> GetById(int id);
        Task Add(WantedResources wantedResources);
        Task<WantedResources> Update(int id, WantedResources wantedResources);
        Task Delete(int id);
    }
}
