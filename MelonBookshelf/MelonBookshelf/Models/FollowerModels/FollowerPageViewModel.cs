namespace MelonBookshelf.Models
{
    public class FollowerPageViewModel
    {
        public FollowerPageViewModel(List<FollowerViewModel> followers)
        {
            Followers = followers;
        }
        public List<FollowerViewModel> Followers = new();
    }
}
