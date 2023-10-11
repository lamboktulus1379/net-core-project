using TestApplication.Infrastructure.ExternalHttpClient.Dto;

namespace TestApplication.Infrastructure.ExternalHttpClient.Tulusfun;

public interface ITulusFunClient
{
    public Task<TypingDto> Execute();
}