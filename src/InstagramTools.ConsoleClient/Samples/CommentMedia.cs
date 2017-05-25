using System;
using InstagramTools.Api.API;

namespace InstagramTools.ConsoleClient.Samples
{
    internal class CommentMedia
    {
        private readonly IInstaApi _instaApi;

        public CommentMedia(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public void DoShow()
        {
            var commentResult = _instaApi.CommentMedia("", "Hi there!");
            Console.WriteLine(commentResult.Succeeded
                ? $"Comment created: {commentResult.Value.Pk}, text: {commentResult.Value.Text}"
                : $"Unable to create comment: {commentResult.Info.Message}");
        }
    }
}