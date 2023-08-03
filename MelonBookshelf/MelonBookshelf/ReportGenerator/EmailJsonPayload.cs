using MimeKit;
using System.Text.Json.Serialization;

namespace MelonBookshelf.ReportGenerator
{
    public class EmailJsonPayload
    {
        public List<string>? ToEmails { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string? FilePathAttachments { get; set; }
    }
}
