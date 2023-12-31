﻿using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByUserName(string name);
        Task<User> Update(User user);
        Task Delete(string id);
    }
}
