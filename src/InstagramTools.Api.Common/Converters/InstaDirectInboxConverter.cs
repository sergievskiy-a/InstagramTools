using System.Collections.Generic;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaDirectInboxConverter :
        IObjectConverter<InstaDirectInboxContainer, InstaDirectInboxContainerResponse>
    {
        public InstaDirectInboxContainerResponse SourceObject { get; set; }

        public InstaDirectInboxContainer Convert()
        {
            var inbox = new InstaDirectInboxContainer
            {
                PendingRequestsCount = this.SourceObject.PendingRequestsCount,
                SeqId = this.SourceObject.SeqId
            };
            if (this.SourceObject.Subscription != null)
            {
                var converter = ConvertersFabric.GetDirectSubscriptionConverter(this.SourceObject.Subscription);
                inbox.Subscription = converter.Convert();
            }

            if (this.SourceObject.Inbox != null)
            {
                inbox.Inbox = new InstaDirectInbox
                {
                    HasOlder = this.SourceObject.Inbox.HasOlder,
                    UnseenCount = this.SourceObject.Inbox.UnseenCount,
                    UnseenCountTs = this.SourceObject.Inbox.UnseenCountTs
                };

                if (this.SourceObject.Inbox.Threads != null && this.SourceObject.Inbox.Threads.Count > 0)
                {
                    inbox.Inbox.Threads = new List<InstaDirectInboxThread>();
                    foreach (var inboxThread in this.SourceObject.Inbox.Threads)
                    {
                        var converter = ConvertersFabric.GetDirectThreadConverter(inboxThread);
                        inbox.Inbox.Threads.Add(converter.Convert());
                    }
                }
            }

            if (this.SourceObject.PendingUsers == null || this.SourceObject.PendingUsers.Count <= 0) return inbox;
            {
                foreach (var user in this.SourceObject.PendingUsers)
                {
                    var converter = ConvertersFabric.GetUserConverter(user);
                    inbox.PendingUsers.Add(converter.Convert());
                }
            }

            return inbox;
        }
    }
}