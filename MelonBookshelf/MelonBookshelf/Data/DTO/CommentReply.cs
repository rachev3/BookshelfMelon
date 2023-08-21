using MelonBookshelf.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Data.DTO
{
    public class CommentReply
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReplyId { get; set; }
        public int ResourceCommentId { get; set; }
        [ForeignKey(nameof(ResourceCommentId))]
        public virtual ResourceComment ResourceComment { get; set; }

        [Column("UserId")]
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        public string Reply { get; set; }
    }
}
