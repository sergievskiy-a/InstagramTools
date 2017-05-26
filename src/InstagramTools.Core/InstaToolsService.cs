using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using InstagramTools.Api.Common.Models;
using InstagramTools.Common;
using InstagramTools.Common.Interfaces;
using InstagramTools.Common.Models;
using InstagramTools.Common.Utils;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core
{
    public class InstaToolsService : IInstaToolsService
    {
        //TODO: To config/constants
        private static readonly int _maxDescriptionLength = 20;

        private readonly ILogger<InstaToolsService> _logger;
        private readonly IInstaApiBuilder _apiBuilder;
        private IInstaApi _instaApi;

        public InstaToolsService(ILogger<InstaToolsService> logger, IInstaApiBuilder apiBuilder)
        {
            _logger = logger;
            _apiBuilder = apiBuilder;
        }

        public async Task<OperationResult> BuildApiManagerAsync(LoginModel loginModel)
        {
            try
            {

                _instaApi = _apiBuilder.SetUser(new UserSessionData
                    {
                        UserName = loginModel.Username, Password = loginModel.Password
                    }).Build();

                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                    throw new Exception($"Unable to login: {logInResult.Info.Message}. [username:{loginModel.Username}]");

                _logger.LogInformation($"Login success! [username:{loginModel.Username}]");
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new OperationResult(false, ex.Message);
            }

        }

        public Task<OperationResult<ProfileModel>> GetLikesFromPhotosByPageIdAsync(int maxPhoto, int pageId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> FollowUsersWhichLikeLastPostAsync(string username, int usersToFollow)
        {
            try
            {
                var getUserMediaResult = await _instaApi.GetUserMediaAsync(username);
                if (!getUserMediaResult.Succeeded)
                {
                    throw new Exception($"Can't get media [username:{username}]. Error:\t{getUserMediaResult.Info.Message }");
                }

                var userMedia = getUserMediaResult.Value;
                var lastMedia = userMedia.OrderByDescending(x => x.DeviceTimeStap /*x.TakenAt*/).FirstOrDefault();

                var unfollowedLikersIds = lastMedia.Likers.Where(x => !x.FriendshipStatus.IncomingRequest).Select(x => x.InstaIdentifier);

                _logger.LogInformation($"FollowUsersWhichLikeLastPost() success! [username:{username}]");
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new OperationResult(false, ex.Message);
            }
            
        }
    }
}
