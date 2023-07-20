namespace MelonBookshelf.Models.Email
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
