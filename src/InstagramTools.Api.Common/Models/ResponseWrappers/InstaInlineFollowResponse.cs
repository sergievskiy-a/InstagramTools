using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaInlineFollowResponse
    {
        [JsonProperty("outgoing_request")]
        public bool IsOutgoingRequest { get; set; }

        [JsonProperty("following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("user_info")]
        public InstaUserResponse UserInfo { get; set; }
    }
}