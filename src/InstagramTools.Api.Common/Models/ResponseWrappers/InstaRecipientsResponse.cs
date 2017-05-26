using System.Collections.Generic;
using InstagramTools.Api.Common.Models.ResponseWrappers.BaseResponse;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class InstaRecipientsResponse : BaseStatusResponse
    {
        public List<InstaDirectInboxThreadResponse> Threads { get; set; } = new List<InstaDirectInboxThreadResponse>();

        public List<InstaUserResponse> Users { get; set; } = new List<InstaUserResponse>();
    }
}