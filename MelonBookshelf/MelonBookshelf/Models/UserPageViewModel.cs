namespace MelonBookshelf.Models
{
    public class UserPageViewModel
    {
        public UserPageViewModel(List<UserViewModel> userViewModels)
        {
            UserViewModels = userViewModels;
        }
        public List<UserViewModel> UserViewModels = new();
    }
}
