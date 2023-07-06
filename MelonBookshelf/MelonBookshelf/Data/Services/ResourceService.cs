using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MelonBookshelf.Data.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _appDbContext;

        public ResourceService (ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Resource resource)
        {
            await _appDbContext.Resources.AddAsync(resource);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var result = await _appDbContext.Resources.FirstOrDefaultAsync(n => n.ResourceId == id);
            _appDbContext.Resources.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Resource>> GetAll()
        {
            var result = await _appDbContext.Resources.ToListAsync();
            return result;
        }

        public async Task<Resource> GetById(int id)
        {
            var result = await _appDbContext.Resources.FirstOrDefaultAsync(n => n.ResourceId == id);
            return result;
        }

        public async Task<Resource> Update(int id, Resource resource)
        {
            _appDbContext.Update(resource);
            await _appDbContext.SaveChangesAsync();
            return resource;
        }

    }
}

