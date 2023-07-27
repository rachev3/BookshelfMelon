using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Data.DTO
{
    public class ResourceDownloadHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DownloadHistoryId { get; set; }

        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string Username { get; set; }
        public DateTime DownloadDate { get; set; }
    }
}
