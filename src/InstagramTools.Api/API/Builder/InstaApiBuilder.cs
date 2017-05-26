using System;
using System.Net.Http;
using InstagramTools.Api.Common;
using InstagramTools.Api.Common.Models;
using InstagramTools.Api.Common.Models.Android.DeviceInfo;
using InstagramTools.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Api.API.Builder
{
    public class InstaApiBuilder : IInstaApiBuilder
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler = new HttpClientHandler();
        private readonly ILogger<InstaApiBuilder> _logger;
        private ApiRequestMessage _requestMessage;
        private UserSessionData _user;


        public InstaApiBuilder(ILogger<InstaApiBuilder> logger)
        {
            _logger = logger;
        }

        public IInstaApi Build()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient(_httpHandler);
                _httpClient.BaseAddress = new Uri(InstaApiConstants.INSTAGRAM_URL);
            }
            AndroidDevice device = null;

            if (_requestMessage == null)
            {
                device = AndroidDeviceGenerator.GetRandomAndroidDevice();
                _requestMessage = new ApiRequestMessage
                {
                    phone_id = device.PhoneGuid.ToString(),
                    guid = device.DeviceGuid,
                    password = _user?.Password,
                    username = _user?.UserName,
                    device_id = ApiRequestMessage.GenerateDeviceId()
                };
            }
            var instaApi = new InstaApi(_user, _httpClient, _httpHandler, _requestMessage, device);
            return instaApi;
        }

        public IInstaApiBuilder UseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        public IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler)
        {
            _httpHandler = handler;
            return this;
        }

        public IInstaApiBuilder SetUserName(string username)
        {
            _user = new UserSessionData {UserName = username};
            return this;
        }

        public IInstaApiBuilder SetUser(UserSessionData user)
        {
            _user = user;
            return this;
        }

        public IInstaApiBuilder SetApiRequestMessage(ApiRequestMessage requestMessage)
        {
            _requestMessage = requestMessage;
            return this;
        }
    }
}