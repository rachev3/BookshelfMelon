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

        public async Task<IEnumerable<Request>> GetAll()
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
    }

}