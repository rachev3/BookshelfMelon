using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelf.Data.Services
{
    public class WantedResourcesService :IWantedResourcesService
    {
        private readonly ApplicationDbContext _appDbContext;

        public WantedResourcesService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(WantedResources wantedResources)
        {
            await _appDbContext.WantedResources.AddAsync(wantedResources);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var result = await _appDbContext.WantedResources.FirstOrDefaultAsync(n => n.Id == id);
            _appDbContext.WantedResources.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WantedResources>> GetAll()
        {
            var result = await _appDbContext.WantedResources.ToListAsync();
            return result;
        }

        public async Task<WantedResources> GetById(int id)
        {
            var result = await _appDbContext.WantedResources.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<WantedResources> Update(int id, WantedResources wantedResources)
        {
            _appDbContext.Update(wantedResources);
            await _appDbContext.SaveChangesAsync();
            return wantedResources;
        }
    }
}
