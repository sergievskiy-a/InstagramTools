using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaMediaLikersResponse : BadStatusResponse
    {
        [JsonProperty("users")]
        public List<InstaUserResponse> Users { get; set; }

        [JsonProperty("user_count")]
        public int UsersCount { get; set; }
    }
}