using System;
using System.Linq;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using InstagramTools.Api.Common.Models;
using InstagramTools.Common;
using InstagramTools.Common.Interfaces;
using InstagramTools.Common.Models;
using InstagramTools.Common.Utils;

namespace InstagramTools.Core
{
    public class InstaToolsService: IInstaToolsService
    {
        //TODO: To config/constants
        private static readonly int _maxDescriptionLength = 20;

        private readonly LoginModel _loginModel;
        private readonly ILogger _logger;

        public IInstaApi Api { get; private set; }

        public InstaToolsService(ILogger logger, LoginModel loginModel)
        {
            _logger = logger;
            _loginModel = loginModel;
        }

        public OperationResult BuildApiManager(LoginModel loginModel)
        {
            Api = new InstaApiBuilder()
                .SetUser(new UserSessionData { UserName = loginModel.Username, Password = loginModel.Password })
                .Build();
            var logInResult = Api.Login();
            if (!logInResult.Succeeded)
                return new OperationResult(false, $"Unable to login: {logInResult.Info.Message}");
            return new OperationResult(true);
        }


        public void DoShow()
        {
            // get currently logged in user
            var currentUser = Api.GetCurrentUser().Value;
            Console.WriteLine($"Logged in: username - {currentUser.UserName}, full name - {currentUser.FullName}");

            // get self followers 
            var followers = Api.GetUserFollowersAsync(currentUser.UserName, 5).Result.Value;
            Console.WriteLine($"Count of followers [{currentUser.UserName}]:{followers.Count}");

            // get self user's media, latest 5 pages
            var currentUserMedia = Api.GetUserMedia(currentUser.UserName, 5);
            if (currentUserMedia.Succeeded)
            {
                Console.WriteLine($"Media count [{currentUser.UserName}]: {currentUserMedia.Value.Count}");
                foreach (var media in currentUserMedia.Value)
                    ConsoleHelper.PrintMedia("Self media", media, _maxDescriptionLength);
            }

            //get user time line feed, latest 5 pages
            var userFeed = Api.GetUserTimelineFeed(5);
            if (userFeed.Succeeded)
            {
                Console.WriteLine(
                    $"Feed items (in {userFeed.Value.Pages} pages) [{currentUser.UserName}]: {userFeed.Value.Medias.Count}");
                foreach (var media in userFeed.Value.Medias)
                    ConsoleHelper.PrintMedia("Feed media", media, _maxDescriptionLength);
                //like first 10 medias from user timeline feed
                foreach (var media in userFeed.Value.Medias.Take(10))
                {
                    var likeResult = Api.LikeMedia(media.InstaIdentifier);
                    var resultString = likeResult.Value ? "liked" : "not liked";
                    Console.WriteLine($"Media {media.Code} {resultString}");
                }
            }

            // get tag feed, latest 5 pages
            var tagFeed = Api.GetTagFeed("quadcopter", 5);
            if (tagFeed.Succeeded)
            {
                Console.WriteLine(
                    $"Tag feed items (in {tagFeed.Value.Pages} pages) [{currentUser.UserName}]: {tagFeed.Value.Medias.Count}");
                foreach (var media in tagFeed.Value.Medias)
                    ConsoleHelper.PrintMedia("Tag feed", media, _maxDescriptionLength);
            }
        }

        public OperationResult<ProfileModel> GetLikesFromPhotosByPageId(int maxPhoto, int pageId)
        {
            throw new NotImplementedException();
        }
    }
}
