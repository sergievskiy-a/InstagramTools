using System;
using System.Net.Http;
using InstagramTools.Api.Common;
using InstagramTools.Api.Common.Models;
using InstagramTools.Api.Common.Models.Android.DeviceInfo;
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
            this._logger = logger;
        }

        public IInstaApi Build()
        {
            if (this._httpClient == null)
            {
                this._httpClient = new HttpClient(this._httpHandler);
                this._httpClient.BaseAddress = new Uri(InstaApiConstants.INSTAGRAM_URL);
            }

            AndroidDevice device = null;

            if (this._requestMessage == null)
            {
                device = AndroidDeviceGenerator.GetRandomAndroidDevice();
                this._requestMessage = new ApiRequestMessage
                {
                    phone_id = device.PhoneGuid.ToString(),
                    guid = device.DeviceGuid,
                    password = this._user?.Password,
                    username = this._user?.UserName,
                    device_id = ApiRequestMessage.GenerateDeviceId()
                };
            }

            var instaApi = new InstaApi(this._user, this._httpClient, this._httpHandler, this._requestMessage, device);
            return instaApi;
        }

        public IInstaApiBuilder UseHttpClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            return this;
        }

        public IInstaApiBuilder UseHttpClientHandler(HttpClientHandler handler)
        {
            this._httpHandler = handler;
            return this;
        }

        public IInstaApiBuilder SetUserName(string username)
        {
            this._user = new UserSessionData {UserName = username};
            return this;
        }

        public IInstaApiBuilder SetUser(UserSessionData user)
        {
            this._user = user;
            return this;
        }

        public IInstaApiBuilder SetApiRequestMessage(ApiRequestMessage requestMessage)
        {
            this._requestMessage = requestMessage;
            return this;
        }
    }
}