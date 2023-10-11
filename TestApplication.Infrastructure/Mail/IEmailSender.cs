using TestApplication.Core.Models.Mail;

namespace TestApplication.Infrastructure.Mail;

public interface IEmailSender
{
    void SendEmail(Message message);
}