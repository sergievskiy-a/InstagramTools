using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaRecentActivityFeedResponse
    {
        [JsonProperty("args")]
        public InstaRecentActivityStoryItemResponse Args { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("pk")]
        public string Pk { get; set; }
    }
}