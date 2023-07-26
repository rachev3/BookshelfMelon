namespace MelonBookshelf.Models
{
    public class ResourceCommentPageViewModel
    {
        public ResourceCommentPageViewModel(List<ResourceCommentViewModel> resourceCommentViewModels)
        {
             ResourceCommentViewModels = resourceCommentViewModels;
        }
        public List<ResourceCommentViewModel> ResourceCommentViewModels { get; set; }
    }
}
