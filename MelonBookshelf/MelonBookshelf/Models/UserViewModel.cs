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
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
