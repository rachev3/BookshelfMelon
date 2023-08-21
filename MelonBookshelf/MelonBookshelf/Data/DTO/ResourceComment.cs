using MelonBookshelf.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Data.DTO
{
    public class ResourceComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int ResourceId { get; set; }
        [ForeignKey(nameof(ResourceId))]
        public virtual Resource Resource { get; set; }

        [Column("UserId")]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public virtual List<CommentReply>? CommentsReplys { get; set; }

        public string Comment { get; set; }
    }
}
