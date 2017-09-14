using System;
using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaStoryItemConverter : IObjectConverter<InstaStoryItem, InstaStoryItemResponse>
    {
        public InstaStoryItemResponse SourceObject { get; set; }

        public InstaStoryItem Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var instaStory = new InstaStoryItem
            {
                CanViewerSave = this.SourceObject.CanViewerSave,
                CaptionIsEdited = this.SourceObject.CaptionIsEdited,
                CaptionPosition = this.SourceObject.CaptionPosition,
                ClientCacheKey = this.SourceObject.ClientCacheKey,
                Code = this.SourceObject.Code,
                CommentCount = this.SourceObject.CommentCount,
                CommentsDisabled = this.SourceObject.CommentsDisabled,
                ExpiringAt = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.ExpiringAt),
                FilterType = this.SourceObject.FilterType,
                HasAudio = this.SourceObject.HasAudio,
                HasLiked = this.SourceObject.HasLiked,
                HasMoreComments = this.SourceObject.HasMoreComments,
                Id = this.SourceObject.Id,
                IsReelMedia = this.SourceObject.IsReelMedia,
                LikeCount = this.SourceObject.LikeCount,
                MaxNumVisiblePreviewComments = this.SourceObject.MaxNumVisiblePreviewComments,
                MediaType = this.SourceObject.MediaType,
                OriginalHeight = this.SourceObject.OriginalHeight,
                OriginalWidth = this.SourceObject.OriginalWidth,
                PhotoOfYou = this.SourceObject.PhotoOfYou,
                Pk = this.SourceObject.Pk,
                TakenAt = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.TakenAt),
                TrackingToken = this.SourceObject.TrackingToken,
                VideoDuration = this.SourceObject.VideoDuration,
                VideoVersions = this.SourceObject.VideoVersions
            };

            if (this.SourceObject.User != null)
                instaStory.User = ConvertersFabric.GetUserConverter(this.SourceObject.User).Convert();

            if (this.SourceObject.Caption != null)
                instaStory.Caption = ConvertersFabric.GetCaptionConverter(this.SourceObject.Caption).Convert();

            if (this.SourceObject.Likers?.Count > 0)
                foreach (var liker in this.SourceObject.Likers)
                    instaStory.Likers.Add(ConvertersFabric.GetUserConverter(liker).Convert());

            if (this.SourceObject.CarouselMedia != null)
                instaStory.CarouselMedia = ConvertersFabric.GetCarouselConverter(this.SourceObject.CarouselMedia).Convert();

            if (this.SourceObject.UserTags?.In?.Count > 0)
                foreach (var tag in this.SourceObject.UserTags.In)
                    instaStory.UserTags.Add(ConvertersFabric.GetUserTagConverter(tag).Convert());

            if (this.SourceObject.ImageVersions?.Candidates != null)
                foreach (var image in this.SourceObject.ImageVersions.Candidates)
                    instaStory.Images.Add(new MediaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));

            return instaStory;
        }
    }
}