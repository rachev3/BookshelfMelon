using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelf.Data.Services
{
    public class UserService:IUserService
    {
        private readonly ApplicationDbContext _appDbContext;

        public UserService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(User user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(string id)
        {
            var result = await _appDbContext.Users.FirstOrDefaultAsync(n => n.Id == id);
            _appDbContext.Users.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            var result = await _appDbContext.Users.ToListAsync();
            return result;
        }

        public async Task<User> GetById(string id)
        {
            var result = await _appDbContext.Users.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<User> Update(string id, User user)
        {
            _appDbContext.Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

    }
}
