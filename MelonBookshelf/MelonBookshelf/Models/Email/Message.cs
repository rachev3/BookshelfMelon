using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;
using Humanizer;
using System.Text.Json.Serialization;

namespace MelonBookshelf.Models.Email
{
    public class Message
    {
        public List<string> ToEmails { get; set; }
        [JsonIgnore]
        public List<MailboxAddress>? To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message()
        {
           
        }
        public Message(List<string> emails,IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress("MelonBookshelf",x)));
            ToEmails = emails;
            Subject = subject;
            Content = content;
        }
        public Message(List<string> emails, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(emails.Select(x => new MailboxAddress("MelonBookshelf", x)));
            ToEmails = emails;
            Subject = subject;
            Content = content;
        }
        
        public void Convert(List<string> emails)
        {
            To = new List<MailboxAddress>();

            To.AddRange(emails.Select(x => new MailboxAddress("MelonBookshelf", x)));
        }
    }
}
