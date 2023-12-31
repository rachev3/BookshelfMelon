﻿using MelonBookshelf.Models;
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
            var result = await _appDbContext.Requests
                .Include(ca => ca.Category)
                .Include(a => a.Upvotes).ThenInclude(b => b.User)
                .Include(c => c.Followers).ThenInclude(d => d.User)
                .ToListAsync();
            return result;
        }

        public async Task<List<Request>> GetPendingRequests()
        {
            var result = await _appDbContext.Requests.Where(r => r.Status == RequestStatus.PendingConfirmation)
                .Include(ca => ca.Category)
                .Include(u => u.User)
                .Include(a => a.Upvotes).ThenInclude(b => b.User)
                .Include(c => c.Followers).ThenInclude(d => d.User)
                .ToListAsync();
            return result;
        }

        public async Task<List<Request>> GetMyRequests(string userId)
        {
            var result = await _appDbContext.Requests.Where(r => r.UserId == userId)
                .Include(ca => ca.Category)
                .Include(a => a.Upvotes).ThenInclude(b => b.User)
                .Include(c => c.Followers)
                .ToListAsync();
            return result;
        }

        public async Task<List<Request>> GetFollowingRequests(string userId)
        {
            var wr = await _appDbContext.Followers.Where(w => w.UserId == userId).ToListAsync();
            List<Request> requests = new();

            foreach (var item in wr)
            {
                requests.Add(await _appDbContext.Requests
                    .Include(ca => ca.Category)
                    .FirstOrDefaultAsync(r => r.RequestId == item.RequestId));
            }

            return requests;
        }

        public async Task<List<string>> GetFollowersEmails(int requestId)
        {
            var followers = await _appDbContext.Followers.Where(r => r.RequestId == requestId).ToListAsync();
            List<string> emails = new List<string>();

            foreach (var item in followers)
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == item.UserId);
                emails.Add(user.Email);
            }

            return emails;
        }

        public async Task<Request> GetById(int id)
        {
            var result = await _appDbContext.Requests.Include(x => x.Category)
                           .Include(x => x.Upvotes)
                           .Include(x => x.Followers)
                           .FirstOrDefaultAsync(n => n.RequestId == id);

            return result;
        }

        public async Task<Request> Update(Request request)
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

            var like = await _appDbContext.Upvotes.FirstOrDefaultAsync(u => u.UserId == userId && u.RequestId == requestId);

            if (like == null)
            {
                await _appDbContext.Upvotes.AddAsync(upvoter);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task Dislike(int requestId, string userId)
        {
            var result = await _appDbContext.Upvotes.FirstOrDefaultAsync(n => n.RequestId == requestId && n.UserId == userId);

            if (result != null)
            {
                _appDbContext.Upvotes.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task Follow(int requestId, string userId)
        {
            Follower follower = new();
            follower.RequestId = requestId;
            follower.UserId = userId;

            var follow = await _appDbContext.Followers.FirstOrDefaultAsync(f => f.UserId == userId && f.RequestId == requestId);

            if (follow == null)
            {
                await _appDbContext.Followers.AddAsync(follower);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task UnFollow(int requestId, string userId)
        {
            var result = await _appDbContext.Followers.FirstOrDefaultAsync(n => n.RequestId == requestId && n.UserId == userId);

            if (result != null)
            {
                _appDbContext.Followers.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }

}