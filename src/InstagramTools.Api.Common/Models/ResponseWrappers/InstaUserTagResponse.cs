using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaUserTagResponse
    {
        [JsonProperty("position")]
        public double[] Position { get; set; }

        [JsonProperty("time_in_video")]
        public string TimeInVideo { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }
    }
}