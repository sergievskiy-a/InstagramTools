using System.Net.Http;
using InstagramTools.Api.Common.Models;

namespace InstagramTools.Api.API.Builder
{
    public interface IInstaApiBuilder
    {
        IInstaApi Build();
        IInstaApiBuilder UseHttpClient(HttpClient httpClient);
        IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler);
        IInstaApiBuilder SetUserName(string username);
        IInstaApiBuilder SetUser(UserSessionData user);
    }
}