using MelonBookshelf.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelonBookshelf.Models
{
    public class ResourceViewModel
    {
        public ResourceViewModel()
        {

        }
        public ResourceViewModel(Resource resource)
        {
            ResourceId = resource.ResourceId;
            Type = resource.Type;
            Author = resource.Author;
            Title = resource.Title;
            Description = resource.Description;
            Location = resource.Location;
            Price = resource.Price;
            Invoice = resource.Invoice;
            Status = resource.Status;
            DateAdded = resource.DateAdded;
            DateTaken = resource.DateTaken;
            DateReturn = resource.DateReturn;
            wantedResources = resource.WantedResources;
        }

        public int ResourceId { get; set; }
        public ResourceType Type { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public double? Price { get; set; }
        public string? Invoice { get; set; }
        public ResourceStatus? Status { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateTaken { get; set; }
        public DateTime? DateReturn { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<WantedResources>? wantedResources { get; set; }
    
    }
}
