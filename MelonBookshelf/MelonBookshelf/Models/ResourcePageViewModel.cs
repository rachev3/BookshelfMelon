namespace MelonBookshelf.Models
{
    public class ResourcePageViewModel
    {
        public ResourcePageViewModel(List<ResourceViewModel> resources, List<CategoryViewModel> categories)
        {
            Resources = resources;
            Categories = categories;
        }
        public List<ResourceViewModel> Resources = new();
        public List<CategoryViewModel> Categories = new();
    }
}
