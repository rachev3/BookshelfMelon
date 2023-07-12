using MelonBookshelf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelf.Data.Services
{
    public class UserService:IUserService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> userManager;

        public UserService(ApplicationDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            this.userManager = userManager;
        }

        public async Task Add(User user,string password)
        {
            await userManager.CreateAsync(user,password);
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

        public async Task<User> GetByName(string name)
        {
            var result = await _appDbContext.Users.FirstOrDefaultAsync(u=>u.UserName == name);
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
