using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaStoryMediaResponse
    {
        [JsonProperty("media")]
        public InstaStoryItemResponse Media { get; set; }
    }
}
