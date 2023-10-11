using TestApplication.Core.Models.Mail;
using TestApplication.Infrastructure.ExternalHttpClient.Tulusfun;
using TestApplication.Infrastructure.Mail;

namespace TestApplication.Usecase;

public class TestUsecase : ITestUsecase
{
    private readonly IEmailSender _emailSender;
    private readonly ITulusFunClient _client;

    public TestUsecase(IEmailSender emailSender, ITulusFunClient client)
    {
        _emailSender = emailSender;
        _client = client;
    }
    
    public bool SendMail(string msg)
    {
        var message = new Message(new string[] { "lamboktulus1379@gmail.com" }, "Test email", msg);
        // _emailSender.SendEmail(message);

        var typingDto =  _client.Execute();
        Console.WriteLine(typingDto.Result.Content);

        return true;
    }
}