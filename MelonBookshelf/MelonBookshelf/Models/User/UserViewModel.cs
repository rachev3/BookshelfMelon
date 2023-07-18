using Microsoft.AspNetCore.Identity;

namespace MelonBookshelf.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email; 
            Id = user.Id;
        }
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
