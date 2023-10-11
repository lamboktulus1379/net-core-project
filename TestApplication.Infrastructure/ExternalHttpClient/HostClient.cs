using System.Text.Json;
using TestApplication.Core.Interfaces;

namespace TestApplication.Infrastructure.ExternalHttpClient;

public class HostClient : IHostClient
{
    private readonly HttpClient _httpClient;
    
    public HostClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> SendRequest(HttpMethod method, string uri, HttpContent? httpContent, Dictionary<string, string> headers)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, uri)
        {
            Content = httpContent
        };

        httpRequestMessage.Headers.Clear();
        foreach (var header in headers)
        {
            httpRequestMessage.Headers.Add(header.Key, header.Value);
        }

        HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
        try
        {
            httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return httpResponseMessage;
    }
}