using System.Collections.Generic;

namespace InstagramTools.Api.Common.Models.Models
{
    public class InstaDirectInboxContainer
    {
        public int PendingRequestsCount { get; set; }

        public int SeqId { get; set; }

        public InstaDirectInboxSubscription Subscription { get; set; }

        public InstaDirectInbox Inbox { get; set; }

        public List<InstaUser> PendingUsers { get; set; }
    }
}