using System;
using InstagramTools.Api.Common.Models.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaDirectInboxItemResponse : BaseStatusResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("media_share")]
        public InstaMediaItemResponse MediaShare { get; set; }

        [JsonProperty("client_context")]
        public Guid ClientContext { get; set; }
    }
}