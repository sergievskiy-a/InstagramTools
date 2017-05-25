﻿using System.Collections.Generic;
using InstagramTools.Api.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaFeedResponse : BaseLoadableResponse
    {
        [JsonProperty("is_direct_v2_enabled")]
        public bool IsDirectV2Enabled { get; set; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public List<InstaMediaItemResponse> Items { get; set; } = new List<InstaMediaItemResponse>();
    }
}