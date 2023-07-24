using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelf.Data.Services
{
    public class UpvoteService : IUpvoteService
    {
        private readonly ApplicationDbContext _appDbContext;

        public UpvoteService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Upvote upvote)
        {
            await _appDbContext.Upvotes.AddAsync(upvote);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var result = await _appDbContext.Upvotes.FirstOrDefaultAsync(n => n.UpvoteId == id);
            _appDbContext.Upvotes.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Upvote>> GetAll()
        {
            var result = await _appDbContext.Upvotes.ToListAsync();
            return result;
        }
       
        public async Task<Upvote> GetById(int id)
        {
            var result = await _appDbContext.Upvotes.FirstOrDefaultAsync(n => n.UpvoteId == id);
            return result;
        }

        public async Task<Upvote> Update(int id, Upvote upvote)
        {
            _appDbContext.Update(upvote);
            await _appDbContext.SaveChangesAsync();
            return upvote;
        }

    }
}
