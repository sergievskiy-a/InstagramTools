using System.Collections.Generic;
using InstagramTools.Api.Classes.ResponseWrappers.BaseResponse;

namespace InstagramTools.Api.Classes.Models
{
    public class InstaRecipients : BaseStatusResponse
    {
        public InstaUserList Users { get; set; } = new InstaUserList();
        public List<InstaDirectInboxThread> Threads { get; set; } = new List<InstaDirectInboxThread>();
    }
}