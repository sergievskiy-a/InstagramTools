using System.Collections.Generic;
using InstagramTools.Api.Classes.ResponseWrappers.BaseResponse;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    internal class InstaRecipientsResponse : BaseStatusResponse
    {
        public List<InstaDirectInboxThreadResponse> Threads { get; set; } = new List<InstaDirectInboxThreadResponse>();

        public List<InstaUserResponse> Users { get; set; } = new List<InstaUserResponse>();
    }
}