﻿using InstagramTools.Api.Common.Models.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaCaptionResponse : BaseStatusResponse
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("created_at_utc")]
        public string CreatedAtUtcUnixLike { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAtUnixLike { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("user")]
        public InstaUserResponse User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("pk")]
        public string Pk { get; set; }
    }
}