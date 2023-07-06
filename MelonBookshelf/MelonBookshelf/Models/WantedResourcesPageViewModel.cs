namespace MelonBookshelf.Models
{
    public class WantedResourcesPageViewModel
    {
        public WantedResourcesPageViewModel( List<WantedResourcesViewModel> wantedResources)
        {
            WantedResources = wantedResources;
        }
        public List<WantedResourcesViewModel> WantedResources = new();
    }
}
