using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MelonBookshelf.Data.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly ApplicationDbContext _appDbContext;
        public FollowerService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Add(Follower follower)
        {
            await _appDbContext.Followers.AddAsync(follower);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var result = await _appDbContext.Followers.FirstOrDefaultAsync(n => n.Id == id);
            _appDbContext.Followers.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Follower>> GetAll()
        {
            var result = await _appDbContext.Followers.ToListAsync();
            return result;
        }

        public async Task<Follower> GetById(int id)
        {
            var result = await _appDbContext.Followers.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<Follower> Update(int id, Follower follower)
        {
            _appDbContext.Update(follower);
            await _appDbContext.SaveChangesAsync();
            return follower;
        }
    }
}
