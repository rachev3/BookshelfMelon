using MelonBookshelf.Data;

namespace MelonBookshelf.Models
{
    public class ResourceEditViewModel
    {
        public ResourceEditViewModel()
        {

        }
        public ResourceEditViewModel(List<CategoryViewModel> categories)
        {

            Categories = categories;
        }
        public ResourceEditViewModel(Resource resource, List<CategoryViewModel> categories)
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
            CategoryId = resource.CategoryId;
            Categories = categories;
            Category = resource.Category;
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

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
    }
}
