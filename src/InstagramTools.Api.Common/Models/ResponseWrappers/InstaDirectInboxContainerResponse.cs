using System.Collections.Generic;
using InstagramTools.Api.Common.Models.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaDirectInboxContainerResponse : BaseStatusResponse
    {
        [JsonProperty("pending_requests_total")]
        public int PendingRequestsCount { get; set; }

        [JsonProperty("seq_id")]
        public int SeqId { get; set; }

        [JsonProperty("subscription")]
        public InstaDirectInboxSubscriptionResponse Subscription { get; set; }

        [JsonProperty("inbox")]
        public InstaDirectInboxResponse Inbox { get; set; }

        [JsonProperty("pending_requests_users")]
        public List<InstaUserResponse> PendingUsers { get; set; }
    }
}