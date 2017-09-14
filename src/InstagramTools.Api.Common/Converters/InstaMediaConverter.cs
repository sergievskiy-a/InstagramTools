using System;
using System.Globalization;
using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaMediaConverter : IObjectConverter<InstaMedia, InstaMediaItemResponse>
    {
        public InstaMediaItemResponse SourceObject { get; set; }

        public InstaMedia Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var media = new InstaMedia
            {
                InstaIdentifier = this.SourceObject.InstaIdentifier,
                Code = this.SourceObject.Code,
                Pk = this.SourceObject.Pk,
                ClientCacheKey = this.SourceObject.ClientCacheKey,
                CommentsCount = this.SourceObject.CommentsCount,
                DeviceTimeStap = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.DeviceTimeStapUnixLike),
                HasLiked = this.SourceObject.HasLiked,
                PhotoOfYou = this.SourceObject.PhotoOfYou,
                TrakingToken = this.SourceObject.TrakingToken,
                TakenAt = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.TakenAtUnixLike),
                Height = this.SourceObject.Height,
                LikesCount = this.SourceObject.LikesCount,
                MediaType = this.SourceObject.MediaType,
                FilterType = this.SourceObject.FilterType,
                Width = this.SourceObject.Width,
                HasAudio = this.SourceObject.HasAudio,
                ViewCount = int.Parse(this.SourceObject.ViewCount.ToString(CultureInfo.InvariantCulture))
            };
            if (this.SourceObject.CarouselMedia != null)
                media.Carousel = ConvertersFabric.GetCarouselConverter(this.SourceObject.CarouselMedia).Convert();
            if (this.SourceObject.User != null) media.User = ConvertersFabric.GetUserConverter(this.SourceObject.User).Convert();
            if (this.SourceObject.Caption != null)
                media.Caption = ConvertersFabric.GetCaptionConverter(this.SourceObject.Caption).Convert();
            if (this.SourceObject.NextMaxId != null) media.NextMaxId = this.SourceObject.NextMaxId;
            if (this.SourceObject.Likers?.Count > 0)
                foreach (var liker in this.SourceObject.Likers)
                    media.Likers.Add(ConvertersFabric.GetUserConverter(liker).Convert());
            if (this.SourceObject.UserTagList?.In?.Count > 0)
                foreach (var tag in this.SourceObject.UserTagList.In)
                    media.Tags.Add(ConvertersFabric.GetUserTagConverter(tag).Convert());
            if (this.SourceObject.Images?.Candidates == null) return media;
            foreach (var image in this.SourceObject.Images.Candidates)
                media.Images.Add(new MediaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
            return media;
        }
    }
}