using MelonBookshelf.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Data.DTO
{
    public class ResourceComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ResourceId { get; set; }
        [ForeignKey(nameof(ResourceId))]
        public virtual Resource Resource { get; set; }

        [Column("Id")]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public string Comment { get; set; }
    }
}
