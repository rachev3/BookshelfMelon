using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Data
{
    public class BackgroundTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BackgroundTaskId { get; set; }
        public string? TaskType { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ExecutionTime { get; set; }
        public string? Payload { get; set; }
    }
}
