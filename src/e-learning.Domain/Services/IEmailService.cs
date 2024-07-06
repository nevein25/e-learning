namespace e_learning.Domain.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage email);

    }
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}
