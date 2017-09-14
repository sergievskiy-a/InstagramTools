using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaCommentListConverter : IObjectConverter<InstaCommentList, InstaCommentListResponse>
    {
        public InstaCommentListResponse SourceObject { get; set; }

        public InstaCommentList Convert()
        {
            var commentList = new InstaCommentList
            {
                Caption = ConvertersFabric.GetCaptionConverter(this.SourceObject.Caption).Convert(),
                CaptionIsEdited = this.SourceObject.CaptionIsEdited,
                CommentsCount = this.SourceObject.CommentsCount,
                LikesEnabled = this.SourceObject.LikesEnabled,
                MoreComentsAvailable = this.SourceObject.MoreComentsAvailable,
                MoreHeadLoadAvailable = this.SourceObject.MoreHeadLoadAvailable
            };
            foreach (var commentResponse in this.SourceObject.Comments)
            {
                var converter = ConvertersFabric.GetCommentConverter(commentResponse);
                commentList.Comments.Add(converter.Convert());
            }

            return commentList;
        }
    }
}