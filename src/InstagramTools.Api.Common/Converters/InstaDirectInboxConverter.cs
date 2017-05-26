﻿using System.Collections.Generic;
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
                PendingRequestsCount = SourceObject.PendingRequestsCount,
                SeqId = SourceObject.SeqId
            };
            if (SourceObject.Subscription != null)
            {
                var converter = ConvertersFabric.GetDirectSubscriptionConverter(SourceObject.Subscription);
                inbox.Subscription = converter.Convert();
            }
            if (SourceObject.Inbox != null)
            {
                inbox.Inbox = new InstaDirectInbox
                {
                    HasOlder = SourceObject.Inbox.HasOlder,
                    UnseenCount = SourceObject.Inbox.UnseenCount,
                    UnseenCountTs = SourceObject.Inbox.UnseenCountTs
                };

                if (SourceObject.Inbox.Threads != null && SourceObject.Inbox.Threads.Count > 0)
                {
                    inbox.Inbox.Threads = new List<InstaDirectInboxThread>();
                    foreach (var inboxThread in SourceObject.Inbox.Threads)
                    {
                        var converter = ConvertersFabric.GetDirectThreadConverter(inboxThread);
                        inbox.Inbox.Threads.Add(converter.Convert());
                    }
                }
            }
            if (SourceObject.PendingUsers == null || SourceObject.PendingUsers.Count <= 0) return inbox;
            {
                foreach (var user in SourceObject.PendingUsers)
                {
                    var converter = ConvertersFabric.GetUserConverter(user);
                    inbox.PendingUsers.Add(converter.Convert());
                }
            }
            return inbox;
        }
    }
}