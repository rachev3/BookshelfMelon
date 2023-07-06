namespace MelonBookshelf.Models
{
    public class ResourcePageViewModel
    {
        public ResourcePageViewModel(List<ResourceViewModel> resources)
        {
            Resources = resources;
        }
        public List<ResourceViewModel> Resources = new();
    }
}
