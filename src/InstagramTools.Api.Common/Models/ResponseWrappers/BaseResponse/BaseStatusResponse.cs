﻿using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers.BaseResponse
{
    public class BaseStatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        public bool IsOk()
        {
            return !string.IsNullOrEmpty(this.Status) && this.Status.ToLower() == "ok";
        }

        public bool IsFail()
        {
            return !string.IsNullOrEmpty(this.Status) && this.Status.ToLower() == "fail";
        }
    }
}