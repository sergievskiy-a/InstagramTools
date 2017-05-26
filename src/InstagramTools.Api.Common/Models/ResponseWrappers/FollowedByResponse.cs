using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}