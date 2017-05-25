using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaUserTagListResponse
    {
        [JsonProperty("in")]
        public List<InstaUserTagResponse> In { get; set; } = new List<InstaUserTagResponse>();
    }
}