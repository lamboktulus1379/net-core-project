using System.Net.Http.Headers;
using System.Text.Json;
using TestApplication.Core.Interfaces;
using TestApplication.Infrastructure.ExternalHttpClient.Dto;

namespace TestApplication.Infrastructure.ExternalHttpClient.Tulusfun;

public class TulusFunClient : ITulusFunClient
{
    private readonly IHostClient _hostClient;
    private readonly string _baseUrl;
    private const string EndpointRandomTyping = "/random";
    private readonly JsonSerializerOptions _options;
    public TulusFunClient(IHostClient hostClient, string baseUrl)
    {
        _hostClient = hostClient;
        _baseUrl = baseUrl;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    
    public async Task<TypingDto> Execute()
    {
        return await GetTypings();
    }

    public async Task<TypingDto> GetTypings()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Accept", "application/json");
        var response = await _hostClient.SendRequest(HttpMethod.Get, $"{_baseUrl}{EndpointRandomTyping}", null, headers);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var typing = JsonSerializer.Deserialize<TypingDto>(content, _options);

        return typing;
    }
}