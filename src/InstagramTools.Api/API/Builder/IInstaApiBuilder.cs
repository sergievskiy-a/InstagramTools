using System.Net.Http;
using InstagramTools.Api.Classes;
using InstagramTools.Api.Logger;

namespace InstagramTools.Api.API.Builder
{
    public interface IInstaApiBuilder
    {
        IInstaApi Build();
        IInstaApiBuilder UseLogger(ILogger logger);
        IInstaApiBuilder UseHttpClient(HttpClient httpClient);
        IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler);
        IInstaApiBuilder SetUserName(string username);
        IInstaApiBuilder SetUser(UserSessionData user);
    }
}