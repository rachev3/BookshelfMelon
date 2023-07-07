using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;
using System;
namespace MelonBookshelf.Data.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _appDbContext;
        public RequestService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Add(Request request)
        {
            await _appDbContext.Requests.AddAsync(request);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var result = await _appDbContext.Requests.FirstOrDefaultAsync(n => n.RequestId == id);
            _appDbContext.Requests.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Request>> GetAll()
        {
            var result = await _appDbContext.Requests.ToListAsync();
            return result;
        }

        public async Task<Request> GetById(int id)
        {
            var result = await _appDbContext.Requests.FirstOrDefaultAsync(n => n.RequestId == id);
            return result;
        }

        public async Task<Request> Update(int id, Request request)
        {
            _appDbContext.Update(request);
            await _appDbContext.SaveChangesAsync();
            return request;
        }
        public async Task Like(int requestId, string userId)
        {
            Upvote upvoter = new Upvote();
            upvoter.RequestId = requestId;
            upvoter.UserId = userId;

            await _appDbContext.Upvotes.AddAsync(upvoter);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Dislike(int requestId, string userId)
        {
            var result = await _appDbContext.Upvotes.FirstOrDefaultAsync(n => n.RequestId == requestId && n.UserId == userId);
            _appDbContext.Upvotes.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Follow(int requestId, string userId)
        {
            Follower follower = new();
            follower.RequestId = requestId;
            follower.UserId = userId;

            await _appDbContext.Followers.AddAsync(follower);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UnFollow(int requestId, string userId)
        {
            var result = await _appDbContext.Followers.FirstOrDefaultAsync(n => n.RequestId == requestId && n.UserId == userId);
            _appDbContext.Followers.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
    }

}