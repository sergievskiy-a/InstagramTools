using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using InstagramTools.Api.Common.Models;
using InstagramTools.Common;
using InstagramTools.Common.Models;
using InstagramTools.Core.Interfaces;
using InstagramTools.Core.Models.ProfileModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core.Implemenations
{
    public class InstaToolsService : MainService<InstaToolsService>, IInstaToolsService
    {
        //TODO: To config/constants
        private static readonly int _maxDescriptionLength = 20;

        private readonly IMapper _mapper;
        private readonly IInstaApiBuilder _apiBuilder;
        private IInstaApi _instaApi;

        #region Constructors

        public InstaToolsService(IConfigurationRoot root, ILogger<InstaToolsService> logger,
            IMemoryCache memoryCache, IInstaApiBuilder apiBuilder, IMapper mapper)
            : base(root, logger, memoryCache, apiBuilder)
        {
            _mapper = mapper;
        }

        #endregion

        private Int32 GetDelay()
        {
            const int minDelaySec = 20;

            var addToDelaySec = new Random().Next(1, 40);
            var result = (minDelaySec + addToDelaySec) * 1000;
            return result;

        }

        public async Task<OperationResult> BuildApiManagerAsync(LoginModel loginModel)
        {
            try
            {

                _instaApi = _apiBuilder.SetUser(new UserSessionData
                {
                    UserName = loginModel.Username,
                    Password = loginModel.Password
                }).Build();

                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                    throw new Exception($"Unable to login: {logInResult.Info.Message}. [username:{loginModel.Username}]");

                Logger.LogInformation($"Login success! [username:{loginModel.Username}]");
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return new OperationResult(false, ex.Message);
            }

        }

        #region Main

        public async Task<OperationResult<ProfileModel>> GetUserByUsername(string username)
        {
            return await ProcessRequestAsync(async () =>
            {
                var getUserResult = await _instaApi.GetUserAsync(username);
                if (!getUserResult.Succeeded)
                {
                    throw new Exception($"Can't get current user's followers. Error:\n{getUserResult.Info.Message }");
                }
                //TODO: Mapping and return
                return new OperationResult<Models.ProfileModels.ProfileModel>(new ProfileModel());
            });
        }

        public async Task<OperationResult<List<ProfileModel>>> GetMyFollowers(int maxPages = 50)
        {
            return await ProcessRequestAsync(async () =>
            {
                var getFollowersResult = await _instaApi.GetCurrentUserFollowersAsync(maxPages);
                if (!getFollowersResult.Succeeded)
                {
                    throw new Exception($"Can't get current user's followers. Error:\n{getFollowersResult.Info.Message }");
                }
                var userFollowers = getFollowersResult.Value;
                //TODO: Mapping and return
                return new OperationResult<List<ProfileModel>>(new List<ProfileModel>());
            });
        }

        public async Task<OperationResult<List<ProfileModel>>> GetUserFollowers(string username, int maxPages = 50)
        {
            return await ProcessRequestAsync(async () =>
            {
                var getFollowersResult = await _instaApi.GetUserFollowersAsync(username, maxPages);
                if (!getFollowersResult.Succeeded)
                {
                    throw new Exception($"Can't get current user's followers. Error:\n{getFollowersResult.Info.Message }");
                }
                var userFollowers = getFollowersResult.Value;
                //TODO: Mapping and return
                return new OperationResult<List<ProfileModel>>(new List<ProfileModel>());
            });
        }

        public async Task<OperationResult> FollowUser(string username)
        {
            return await ProcessRequestAsync(async () =>
            {
                //Get user info
                var getUserResult = await GetUserByUsername(username);
                if (!getUserResult.Success)
                    throw new Exception(getUserResult.Message);
                var userInfo = getUserResult.Model;

                //Follow user
                var followResult = await _instaApi.FollowUserAsync(userInfo.Id);
                if (!followResult.Succeeded)
                {
                    Logger.LogWarning($"Can't follow [username :{username}]. Error:\t {followResult.Info.Message}");
                }
                //TODO: Insert to database
                Logger.LogInformation($"Now you follow: {username} ");
                await Task.Delay(GetDelay());
                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> FollowUsers(IEnumerable<string> usersnames)
        {
            return await ProcessRequestAsync(async () =>
            {
                foreach (string username in usersnames)
                {
                    var followResult = await FollowUser(username);
                }
                Logger.LogInformation($"Following success!");
                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> UnFollowUser(string username)
        {
            return await ProcessRequestAsync(async () =>
            {
                //Get user info
                var getUserResult = await GetUserByUsername(username);
                if (!getUserResult.Success)
                    throw new Exception(getUserResult.Message);
                var userInfo = getUserResult.Model;

                //Follow user
                var followResult = await _instaApi.UnFollowUserAsync(userInfo.Id);
                if (!followResult.Succeeded)
                {
                    Logger.LogWarning($"Can't unfollow [username :{username}]. Error:\t {followResult.Info.Message}");
                }
                //TODO: mark as unfollowed
                Logger.LogInformation($"Now you unfollow: {username} ");
                await Task.Delay(GetDelay());
                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> UnFollowUsers(IEnumerable<string> usersnames)
        {
            return await ProcessRequestAsync(async () =>
            {
                foreach (string username in usersnames)
                {
                    var followResult = await UnFollowUser(username);
                }
                Logger.LogInformation($"Unfollowing success!");
                return new OperationResult(true);
            });
        }

        #endregion

        public async Task<OperationResult> FollowUsersWhichLikeLastPostAsync(string username)
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
                var mediaId = lastMedia.InstaIdentifier;

                var getLikersResult = await _instaApi.GetMediaLikersAsync(mediaId);
                if (!getLikersResult.Succeeded)
                {
                    throw new Exception($"Can't get likers for media [mediaId :{mediaId}]. Error:\t {getLikersResult.Info.Message}");
                }
                var likersIds = getLikersResult.Value.Select(x => x.Pk).ToList();

                //var unfollowResult = await _instaApi.UnFollowUserAsync(testId);

                foreach (string id in likersIds)
                {
                    var followResult = await _instaApi.FollowUserAsync(id);
                    if (!followResult.Succeeded)
                    {
                        throw new Exception($"Can't follow [userId :{id}]. Error:\t {followResult.Info.Message}");
                    }
                    Logger.LogInformation($"Now you follow user with id={id} ");
                    await Task.Delay(GetDelay());
                }

                Logger.LogInformation($"FollowUsersWhichLikeLastPost() success! [username:{username}]");
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return new OperationResult(false, ex.Message);
            }

        }

        public async Task<OperationResult> FollowSubscribersOfUser(string username)
        {
            try
            {
                var getFollowersResult = await _instaApi.GetUserFollowersAsync(username, _maxDescriptionLength);
                if (!getFollowersResult.Succeeded)
                {
                    throw new Exception($"Can't get followers [username:{username}]. Error:\t{getFollowersResult.Info.Message }");
                }


                var followersIds = getFollowersResult.Value.Select(x => x.Pk).ToList();

                foreach (string id in followersIds)
                {
                    var followResult = await _instaApi.FollowUserAsync(id);
                    if (!followResult.Succeeded)
                    {
                        throw new Exception($"Can't follow [userId :{id}]. Error:\t {followResult.Info.Message}");
                    }
                    Logger.LogInformation($"Now you follow user with id={id} ");
                    await Task.Delay(GetDelay());
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return new OperationResult(false, e.Message);
            }


        }

        public async Task<OperationResult> UnfollowUnreciprocalUsers()
        {
            try
            {
                var getFollowersResult = await _instaApi.GetCurrentUserFollowersAsync(_maxDescriptionLength);
                if (!getFollowersResult.Succeeded)
                {
                    throw new Exception($"Can't get followers for current user. Error:\t{getFollowersResult.Info.Message }");
                }

                var followersIds = getFollowersResult.Value.Select(x => x.Pk);
                foreach (string id in followersIds)
                {
                    var followResult = await _instaApi.UnFollowUserAsync(id);
                    if (!followResult.Succeeded)
                    {
                        throw new Exception($"Can't unfollow [userId :{id}]. Error:\t {followResult.Info.Message}");
                    }
                    Logger.LogInformation($"Now you unfollow user with id={id} ");
                    await Task.Delay(GetDelay());
                }
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
