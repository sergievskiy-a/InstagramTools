﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaDirectInboxResponse
    {
        [JsonProperty("has_older")]
        public bool HasOlder { get; set; }

        [JsonProperty("unseen_count_ts")]
        public long UnseenCountTs { get; set; }

        [JsonProperty("unseen_count")]
        public long UnseenCount { get; set; }

        [JsonProperty("threads")]
        public List<InstaDirectInboxThreadResponse> Threads { get; set; }
    }
}