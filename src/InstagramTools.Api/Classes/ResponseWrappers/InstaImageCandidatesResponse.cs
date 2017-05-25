using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaImageCandidatesResponse
    {
        [JsonProperty("candidates")]
        public List<ImageResponse> Candidates { get; set; }
    }
}