using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaCaptionConverter : IObjectConverter<InstaCaption, InstaCaptionResponse>
    {
        public InstaCaptionResponse SourceObject { get; set; }

        public InstaCaption Convert()
        {
            var caption = new InstaCaption
            {
                Pk = this.SourceObject.Pk,
                CreatedAt = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.CreatedAtUnixLike),
                CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.CreatedAtUtcUnixLike),
                MediaId = this.SourceObject.MediaId,
                Text = this.SourceObject.Text,
                User = ConvertersFabric.GetUserConverter(this.SourceObject.User).Convert(),
                UserId = this.SourceObject.UserId
            };
            return caption;
        }
    }
}