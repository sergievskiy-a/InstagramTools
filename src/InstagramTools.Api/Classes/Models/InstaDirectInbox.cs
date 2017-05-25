using System.Collections.Generic;

namespace InstagramTools.Api.Classes.Models
{
    public class InstaDirectInbox
    {
        public bool HasOlder { get; set; }

        public long UnseenCountTs { get; set; }

        public long UnseenCount { get; set; }

        public List<InstaDirectInboxThread> Threads { get; set; }
    }
}