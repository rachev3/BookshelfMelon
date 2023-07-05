using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Models
{
    public class Upvote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpvoteId { get; set; }

        [Column("Id")]
        public string UserId { get; set; }  
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public virtual Request Request { get; set; }
    }
}
