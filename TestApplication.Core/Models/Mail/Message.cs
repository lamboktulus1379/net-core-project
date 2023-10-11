using MimeKit;

namespace TestApplication.Core.Models.Mail;

public class Message
{
    private readonly EmailConfiguration _emailConfiguration;

    public Message(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public Message(IEnumerable<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            
        Subject = subject;
        Content = content;        
    }
}