using System;
using System.Linq;
using InstagramTools.Api.API;
using InstagramTools.Common.Utils;

namespace InstagramTools.ConsoleClient.Samples
{
    internal class Basics
    {
        /// <summary>
        ///     Config values
        /// </summary>
        private static readonly int _maxDescriptionLength = 20;

        private readonly IInstaApi _instaApi;

        public Basics(IInstaApi instaApi)
        {
            this._instaApi = instaApi;
        }

        public void DoShow()
        {
            // get currently logged in user
            var currentUser = this._instaApi.GetCurrentUser().Value;
            Console.WriteLine($"Logged in: username - {currentUser.UserName}, full name - {currentUser.FullName}");

            // get self followers 
            var followers = this._instaApi.GetUserFollowersAsync(currentUser.UserName, 5).Result.Value;
            Console.WriteLine($"Count of followers [{currentUser.UserName}]:{followers.Count}");

            // get self user's media, latest 5 pages
            var currentUserMedia = this._instaApi.GetUserMedia(currentUser.UserName, 5);
            if (currentUserMedia.Succeeded)
            {
                Console.WriteLine($"Media count [{currentUser.UserName}]: {currentUserMedia.Value.Count}");
                foreach (var media in currentUserMedia.Value)
                    ConsoleHelper.PrintMedia("Self media", media, _maxDescriptionLength);
            }
            
            // get user time line feed, latest 5 pages
            var userFeed = this._instaApi.GetUserTimelineFeed(5);
            if (userFeed.Succeeded)
            {
                Console.WriteLine(
                    $"Feed items (in {userFeed.Value.Pages} pages) [{currentUser.UserName}]: {userFeed.Value.Medias.Count}");
                foreach (var media in userFeed.Value.Medias)
                    ConsoleHelper.PrintMedia("Feed media", media, _maxDescriptionLength);

                // like first 10 medias from user timeline feed
                foreach (var media in userFeed.Value.Medias.Take(10))
                {
                    var likeResult = this._instaApi.LikeMedia(media.InstaIdentifier);
                    var resultString = likeResult.Value ? "liked" : "not liked";
                    Console.WriteLine($"Media {media.Code} {resultString}");
                }
            }

            // get tag feed, latest 5 pages
            var tagFeed = this._instaApi.GetTagFeed("quadcopter", 5);
            if (tagFeed.Succeeded)
            {
                Console.WriteLine(
                    $"Tag feed items (in {tagFeed.Value.Pages} pages) [{currentUser.UserName}]: {tagFeed.Value.Medias.Count}");
                foreach (var media in tagFeed.Value.Medias)
                    ConsoleHelper.PrintMedia("Tag feed", media, _maxDescriptionLength);
            }
        }
    }
}