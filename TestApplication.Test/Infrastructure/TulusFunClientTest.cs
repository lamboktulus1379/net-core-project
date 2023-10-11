using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using RichardSzalay.MockHttp;
using TestApplication.Infrastructure.ExternalHttpClient;
using TestApplication.Infrastructure.ExternalHttpClient.Dto;
using TestApplication.Infrastructure.ExternalHttpClient.Tulusfun;
using Xunit;

namespace TestApplication.Test.Infrastructure;

public class TulusFunClientTest
{
    [Fact]
    public async Task Execute()
    {
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var dto = new TypingDto { Id = Guid.NewGuid(), Content = "This is content." };
        var mockResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create<TypingDto>(dto)
        };

        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);
        
        // Inject the handler or client into your application code
        var httpClient = new HttpClient(mockHandler.Object);
        var hostClient = new HostClient(httpClient);
        var sut = new TulusFunClient(hostClient, "https://tulus.fun/api/typings");
        
        // Act
        var actual = await sut.Execute();
        
        // Assert
        Assert.NotNull(actual);
        
        mockHandler.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get)
            ,ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task Execute2()
    {
        MockHttpMessageHandler mockHandler = new MockHttpMessageHandler();
        var dto = new TypingDto { Id = Guid.NewGuid(), Content = "This is content." };
        var mockResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create<TypingDto>(dto)
        };
        var mockRequest = mockHandler
            .Expect(HttpMethod.Get, "https://tulus.fun/api/typings/random")
            .WithHeaders(@"Accept: application/json")
            .Respond(mockResponse.StatusCode, mockResponse.Content);

        var httpClient = mockHandler.ToHttpClient();
        var hostClient = new HostClient(httpClient);
        var sut = new TulusFunClient(hostClient, "https://tulus.fun/api/typings");

        // Act
        var actual = await sut.Execute();
        Console.WriteLine(actual);
        
        
        // Assert
        Assert.NotNull(actual);
        Assert.Equivalent(dto, actual);
        Assert.Equal(1, mockHandler.GetMatchCount(mockRequest));
        mockHandler.VerifyNoOutstandingExpectation();

    }
}