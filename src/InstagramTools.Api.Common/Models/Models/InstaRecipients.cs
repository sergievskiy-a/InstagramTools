using System.Collections.Generic;
using InstagramTools.Api.Common.Models.ResponseWrappers.BaseResponse;

namespace InstagramTools.Api.Common.Models.Models
{
    public class InstaRecipients : BaseStatusResponse
    {
        public InstaUserList Users { get; set; } = new InstaUserList();
        public List<InstaDirectInboxThread> Threads { get; set; } = new List<InstaDirectInboxThread>();
    }
}