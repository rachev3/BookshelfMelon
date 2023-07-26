using MelonBookshelf.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelf.Data.Services
{
    public class ResourceCommentService : IResourceCommentService
    {
        private readonly ApplicationDbContext _appDbContext;
        public ResourceCommentService(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;
        }
        public async Task<List<ResourceComment>> GetAll()
        {
            var result = await _appDbContext.ResourceComments.Include(c=>c.User).ToListAsync();
            await _appDbContext.SaveChangesAsync();
            return result;
        }
        public async Task<ResourceComment> GetById(int id)
        {
            var result = await _appDbContext.ResourceComments.FirstOrDefaultAsync(rc => rc.Id == id);
            await _appDbContext.SaveChangesAsync();
            return result;
        }
        public async Task Add(ResourceComment resourceComment)
        {
            _appDbContext.ResourceComments.Add(resourceComment);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<ResourceComment> Update(ResourceComment resourceComment)
        {
            var result = _appDbContext.Update(resourceComment);
            await _appDbContext.SaveChangesAsync();
            return resourceComment;
        }
        public async Task Delete(int id)
        {
            var result = await _appDbContext.ResourceComments.FirstOrDefaultAsync(n => n.Id == id);
            _appDbContext.ResourceComments.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
