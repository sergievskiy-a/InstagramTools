using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaLoginResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("logged_in_user")]
        public InstaUserResponse User { get; set; }
    }
}