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
                Caption = ConvertersFabric.GetCaptionConverter(SourceObject.Caption).Convert(),
                CaptionIsEdited = SourceObject.CaptionIsEdited,
                CommentsCount = SourceObject.CommentsCount,
                LikesEnabled = SourceObject.LikesEnabled,
                MoreComentsAvailable = SourceObject.MoreComentsAvailable,
                MoreHeadLoadAvailable = SourceObject.MoreHeadLoadAvailable
            };
            foreach (var commentResponse in SourceObject.Comments)
            {
                var converter = ConvertersFabric.GetCommentConverter(commentResponse);
                commentList.Comments.Add(converter.Convert());
            }
            return commentList;
        }
    }
}