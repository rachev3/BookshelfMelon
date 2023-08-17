using MelonBookshelf.Data.DTO;
using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MelonBookshelf.Data.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _appDbContext;

        public ResourceService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Resource resource)
        {
            await _appDbContext.Resources.AddAsync(resource);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AddDownload(ResourceDownloadHistory download)
        {
            await _appDbContext.ResourceDownloadHistory.AddAsync(download);
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
            var result = await _appDbContext.Resources.Include(c=>c.Comments)
                .Include(c=>c.Category)
               .Include(a => a.WantedResources).ThenInclude(b => b.User)
               .ToListAsync();
            return result;
        }

        public async Task<Resource> GetById(int id)
        {
            var result = await _appDbContext.Resources.Include(c=>c.Category).Include(c=>c.Comments).ThenInclude(u=>u.User).FirstOrDefaultAsync(n => n.ResourceId == id);
            return result;
        }
        public async Task<Resource> GetByName(string name)
        {
            var result = await _appDbContext.Resources.Include(c => c.Category).Include(c => c.Comments).ThenInclude(u => u.User).FirstOrDefaultAsync(n => n.Title == name);
            return result;
        }

        public async Task<List<Resource>> Search(string? title, ResourceType? type, int? categoryId)
        {
            var result = await _appDbContext.Resources.ToListAsync();

            if (title != null) result = result.Where(r => r.Title.Contains(title)).ToList();
            if (type != null) result = result.Where(r => r.Type == type).ToList();
            if (categoryId != null) result = result.Where(r => r.CategoryId == categoryId).ToList();

            return result;
        }

        public async Task Want(string userId, int resourceId)
        {
            WantedResources wantedResources = new ();
            wantedResources.ResourceId = resourceId;
            wantedResources.UserId = userId;

            var want = await _appDbContext.WantedResources.FirstOrDefaultAsync(wr => wr.UserId == userId && wr.ResourceId == resourceId);

            if (want == null)
            {
                await _appDbContext.WantedResources.AddAsync(wantedResources);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task Unwant(string userId, int resourceId)
        {
            var result = await _appDbContext.WantedResources.FirstOrDefaultAsync(n => n.ResourceId == resourceId && n.UserId == userId);
            if (result != null)
            {
                _appDbContext.WantedResources.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Resource> Update(int id, Resource resource)
        {
            _appDbContext.Update(resource);
            await _appDbContext.SaveChangesAsync();
            return resource;
        }

        public async Task<List<ResourceDownloadHistory>> ReportData(DateTime date)
        {
            var result = await _appDbContext.ResourceDownloadHistory.Where(r => r.DownloadDate.Date == date.Date).ToListAsync();
            return result;
        }
    }
}

