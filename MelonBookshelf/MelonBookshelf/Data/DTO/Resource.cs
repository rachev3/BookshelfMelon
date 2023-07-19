using MelonBookshelf.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Models
{
    public class Resource
    {
        public Resource()
        {

        }
        public Resource(ResourceType type, string? author, string? title, string? description, string? location, double? price, string? invoice, ResourceStatus? status, DateTime? dateAdded, DateTime? dateTaken, DateTime? dateReturn, string? userId, User? user, List<WantedResources>? wantedResources, int? categoryId, Category? category)
        {
            Type = type;
            Author = author;
            Title = title;
            Description = description;
            Location = location;
            Price = price;
            Invoice = invoice;
            Status = status;
            DateAdded = dateAdded;
            DateTaken = dateTaken;
            DateReturn = dateReturn;
            UserId = userId;
            User = user;
            WantedResources = wantedResources;
            CategoryId = categoryId;
            Category = category;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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


        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
        public virtual List<WantedResources>? WantedResources { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
    }
}
