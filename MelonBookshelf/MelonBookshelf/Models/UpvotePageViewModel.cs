namespace MelonBookshelf.Models
{
    public class UpvotePageViewModel
    {
        public UpvotePageViewModel(List<UpvoteViewModel> upvoteViewModels)
        {
           UpvoteViewModels = upvoteViewModels;
        }
        public List<UpvoteViewModel> UpvoteViewModels = new();
    }
}
