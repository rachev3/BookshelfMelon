using MelonBookshelf.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Data.DTO
{
    public class CommentReplay
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReplayId { get; set; }
        public int ResourceCommentId { get; set; }
        [ForeignKey(nameof(ResourceCommentId))]
        public virtual ResourceComment ResourceComment { get; set; }

        [Column("Id")]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public string Replay { get; set; }
    }
}
