using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class FollowedByResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}