using System;
using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaCommentConverter
        : IObjectConverter<InstaComment, InstaCommentResponse>
    {
        public InstaCommentResponse SourceObject { get; set; }

        public InstaComment Convert()
        {
            var comment = new InstaComment
            {
                BitFlags = this.SourceObject.BitFlags,
                ContentType = (InstaContentType) Enum.Parse(typeof(InstaContentType), this.SourceObject.ContentType, true),
                CreatedAt = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.CreatedAt),
                CreatedAtUtc = DateTimeHelper.UnixTimestampToDateTime(this.SourceObject.CreatedAtUtc),
                LikesCount = this.SourceObject.LikesCount,
                Pk = this.SourceObject.Pk,
                Status = this.SourceObject.Status,
                Text = this.SourceObject.Text,
                Type = this.SourceObject.Type,
                UserId = this.SourceObject.UserId,
                User = ConvertersFabric.GetUserConverter(this.SourceObject.User).Convert()
            };
            return comment;
        }
    }
}