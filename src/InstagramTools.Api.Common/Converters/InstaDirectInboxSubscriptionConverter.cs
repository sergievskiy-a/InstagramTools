﻿using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaDirectInboxSubscriptionConverter :
        IObjectConverter<InstaDirectInboxSubscription, InstaDirectInboxSubscriptionResponse>
    {
        public InstaDirectInboxSubscriptionResponse SourceObject { get; set; }

        public InstaDirectInboxSubscription Convert()
        {
            var subscription = new InstaDirectInboxSubscription
            {
                Auth = SourceObject.Auth,
                Sequence = SourceObject.Sequence,
                Topic = SourceObject.Topic,
                Url = SourceObject.Url
            };
            return subscription;
        }
    }
}