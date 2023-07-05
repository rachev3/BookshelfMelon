using MelonBookshelf.Data;
using Microsoft.AspNetCore.Identity;

namespace MelonBookshelf.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
