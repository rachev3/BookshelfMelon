using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Models
{
    public class WantedResourcesViewModel
    {
        public WantedResourcesViewModel()
        {

        }
        public WantedResourcesViewModel(WantedResources wantedResources)
        {           
            ResourceId = wantedResources.ResourceId;
            UserId = wantedResources.UserId;
        }

        public int ResourceId { get; set; }
        public string UserId { get; set; }
    }
}
