﻿using InstagramTools.Api.Classes.ResponseWrappers.BaseResponse;
using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaFriendshipStatusResponse : BaseStatusResponse
    {
        [JsonProperty("following")]
        public bool Following { get; set; }

        [JsonProperty("followed_by")]
        public bool FollowedBy { get; set; }

        [JsonProperty("blocking")]
        public bool Blocking { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("incoming_request")]
        public bool IncomingRequest { get; set; }

        [JsonProperty("outgoing_request")]
        public bool OutgoingRequest { get; set; }
    }
}