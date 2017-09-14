using System;
using InstagramTools.Api.API;

namespace InstagramTools.ConsoleClient.Samples
{
    internal class CommentMedia
    {
        private readonly IInstaApi _instaApi;

        public CommentMedia(IInstaApi instaApi)
        {
            this._instaApi = instaApi;
        }

        public void DoShow()
        {
            var commentResult = this._instaApi.CommentMedia(string.Empty, "Hi there!");
            Console.WriteLine(commentResult.Succeeded
                ? $"Comment created: {commentResult.Value.Pk}, text: {commentResult.Value.Text}"
                : $"Unable to create comment: {commentResult.Info.Message}");
        }
    }
}