using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Models
{
    public class UserRegisterViewModel
    {       
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string ConfirmPassword { get; set; }

    }
}
