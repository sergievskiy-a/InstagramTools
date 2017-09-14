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
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var story = new InstaStory
            {
                CanReply = this.SourceObject.CanReply,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.ExpiringAt),
                Id = this.SourceObject.Id,
                LatestReelMedia = this.SourceObject.LatestReelMedia,
                RankedPosition = this.SourceObject.RankedPosition,
                Seen = this.SourceObject.Seen,
                SeenRankedPosition = this.SourceObject.SeenRankedPosition,
                Muted = this.SourceObject.Muted,
                SourceToken = this.SourceObject.SourceToken,
                PrefetchCount = this.SourceObject.PrefetchCount,
                SocialContext = this.SourceObject.SocialContext
            };

            if (this.SourceObject.User != null)
                story.User = ConvertersFabric.GetUserConverter(this.SourceObject.User).Convert();

            if (this.SourceObject.Items?.Count > 0)
                foreach (var InstaStory in this.SourceObject.Items)
                    story.Items.Add(ConvertersFabric.GetStoryItemConverter(InstaStory).Convert());

            return story;
        }
    }
}