using MelonBookshelf.Data;
using Microsoft.AspNetCore.Identity;

namespace Bookshelf.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
