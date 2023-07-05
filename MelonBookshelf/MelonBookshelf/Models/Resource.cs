using MelonBookshelf.Data;

namespace Bookshelf.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }
        public ResourceType Type { get; set; }
        public string Author { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public string Invoice { get; set; }
        public ResourceStatus Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime DateReturn { get; set; }
        
        public int CategoryId { get; set; }
    }
}
