using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    class InstaStoryMediaResponse
    {
        [JsonProperty("media")]
        public InstaStoryItemResponse Media { get; set; }
    }
}
