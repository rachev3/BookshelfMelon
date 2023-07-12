using MelonBookshelf.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public RequestStatus? Status { get; set; }
        public ResourceType? Type { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public string? Motive { get; set; }
        public DateTime? DateAdded { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public virtual List<Upvote>? Upvotes { get; set; }
        public virtual List<Follower>? Followers { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

    }
}
