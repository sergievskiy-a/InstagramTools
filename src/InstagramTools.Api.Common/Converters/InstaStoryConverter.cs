using System;
using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaStoryConverter : IObjectConverter<InstaStory, InstaStoryResponse>
    {
        public InstaStoryResponse SourceObject { get; set; }

        public InstaStory Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var story = new InstaStory
            {
                CanReply = SourceObject.CanReply,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(SourceObject.ExpiringAt),
                Id = SourceObject.Id,
                LatestReelMedia = SourceObject.LatestReelMedia,
                RankedPosition = SourceObject.RankedPosition,
                Seen = SourceObject.Seen,
                SeenRankedPosition = SourceObject.SeenRankedPosition,
                Muted = SourceObject.Muted,
                SourceToken = SourceObject.SourceToken,
                PrefetchCount = SourceObject.PrefetchCount,
                SocialContext = SourceObject.SocialContext
            };

            if (SourceObject.User != null)
                story.User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert();

            if (SourceObject.Items?.Count > 0)
                foreach (var InstaStory in SourceObject.Items)
                    story.Items.Add(ConvertersFabric.GetStoryItemConverter(InstaStory).Convert());

            return story;
        }
    }
}