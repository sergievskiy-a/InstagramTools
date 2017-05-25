using InstagramTools.Api.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaCurrentUserResponse : BaseStatusResponse
    {
        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
    }
}