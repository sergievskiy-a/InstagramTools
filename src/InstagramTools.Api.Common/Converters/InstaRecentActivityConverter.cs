using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaRecentActivityConverter :
        IObjectConverter<InstaRecentActivityFeed, InstaRecentActivityFeedResponse>
    {
        public InstaRecentActivityFeedResponse SourceObject { get; set; }

        public InstaRecentActivityFeed Convert()
        {
            var activityStory = new InstaRecentActivityFeed
            {
                Pk = this.SourceObject.Pk,
                Type = this.SourceObject.Type,
                ProfileId = this.SourceObject.Args.ProfileId,
                ProfileImage = this.SourceObject.Args.ProfileImage,
                Text = this.SourceObject.Args.Text,
                TimeStamp = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.Args.TimeStamp)
            };
            if (this.SourceObject.Args.Links != null)
                foreach (var instaLinkResponse in this.SourceObject.Args.Links)
                    activityStory.Links.Add(new InstaLink
                    {
                        Start = instaLinkResponse.Start,
                        End = instaLinkResponse.End,
                        Id = instaLinkResponse.Id,
                        Type = instaLinkResponse.Type
                    });
            if (this.SourceObject.Args.InlineFollow != null)
            {
                activityStory.InlineFollow = new InstaInlineFollow
                {
                    IsFollowing = this.SourceObject.Args.InlineFollow.IsFollowing,
                    IsOutgoingRequest = this.SourceObject.Args.InlineFollow.IsOutgoingRequest
                };
                if (this.SourceObject.Args.InlineFollow.UserInfo != null)
                    activityStory.InlineFollow.User =
                        ConvertersFabric.GetUserConverter(this.SourceObject.Args.InlineFollow.UserInfo).Convert();
            }

            return activityStory;
        }
    }
}