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
            PhoneNumber = user.PhoneNumber;
            Username = user.UserName;
            City = user.City;
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
