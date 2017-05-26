using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaImageCandidatesResponse
    {
        [JsonProperty("candidates")]
        public List<ImageResponse> Candidates { get; set; }
    }
}