namespace TestApplication.Core.Interfaces;

public interface IHostClient
{
    public Task<HttpResponseMessage> SendRequest(HttpMethod method, string uri, HttpContent? httpContent, Dictionary<string, string> headers);
}