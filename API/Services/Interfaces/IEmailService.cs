using API.Helpers;
using System.Net.Mail;

namespace API.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage email);

    }
}
