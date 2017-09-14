using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using InstagramTools.Api.Common.Models;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Common.Helpers;
using InstagramTools.Common.Models;
using InstagramTools.Core.Interfaces;
using InstagramTools.Core.Models.ProfileModels;
using InstagramTools.Data;
using InstagramTools.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InstagramTools.Core.Implemenations
{
    public class InstaToolsService : MainService<InstaToolsService>, IInstaToolsService
    {
        // TODO: To config/constants
        private static readonly int _maxDescriptionLength = 20;

        private readonly IInstaApiBuilder apiBuilder;
        private IInstaApi instaApi;

        #region Constructors

        public InstaToolsService(IConfigurationRoot root, ILogger<InstaToolsService> logger,
            IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context, IInstaApiBuilder apiBuilder)
            : base(root, logger, memoryCache, mapper, context)
        {
            this.apiBuilder = apiBuilder;
        }

        #endregion

        private int GetDelay()
        {
            // TODO: To settings file.
            const int MinDelaySec = 25;

            var addToDelaySec = new Random().Next(1, 40);
            var result = (MinDelaySec + addToDelaySec) * 1000;
            return result;
        }

        public async Task<OperationResult> BuildApiManagerAsync(LoginModel loginModel)
        {
            try
            {
                this.instaApi = this.apiBuilder.SetUser(new UserSessionData
                {
                    UserName = loginModel.Username,
                    Password = loginModel.Password
                }).Build();

                var logInResult = await this.instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                    throw new Exception($"Unable to login: {logInResult.Info.Message}. [username:{loginModel.Username}]");

                this.Logger.LogInformation($"Login success! [username:{loginModel.Username}]");
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex.Message);
                return new OperationResult(false, ex.Message);
            }

        }

        #region Main

        public async Task<OperationResult<InstProfile>> GetProfileByUsername(string username)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var getUserResult = await this.instaApi.GetUserAsync(username);
                if (!getUserResult.Succeeded)
                {
                    throw new Exception($"Can't get current user's followers. Error:\n{getUserResult.Info.Message }");
                }

                var profile = this.Mapper.Map<InstaUser, InstProfile>(getUserResult.Value);

                return new OperationResult<InstProfile>(profile);
            });
        }

        public async Task<OperationResult<List<InstProfile>>> GetMyFollowers(int maxPages = 50)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var getFollowersResult = await this.instaApi.GetCurrentUserFollowersAsync(maxPages);
                if (!getFollowersResult.Succeeded)
                {
                    throw new Exception($"Can't get current user's followers. Error:\n{getFollowersResult.Info.Message }");
                }

                var followers = this.Mapper.Map<InstaUserList, List<InstProfile>>(getFollowersResult.Value);

                return new OperationResult<List<InstProfile>>(followers);
            });
        }

        public async Task<OperationResult<List<InstProfile>>> GetFollowersByUsername(string username, int maxPages = 50)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var getFollowersResult = await this.instaApi.GetUserFollowersAsync(username, maxPages);
                if (!getFollowersResult.Succeeded)
                {
                    throw new Exception($"Can't get current user's followers. Error:\n{getFollowersResult.Info.Message }");
                }

                var followers = this.Mapper.Map<InstaUserList, List<InstProfile>>(getFollowersResult.Value);

                return new OperationResult<List<InstProfile>>(followers);
            });
        }

        public async Task<OperationResult> FollowUser(string username)
        {
            return await this.ProcessRequestAsync(async () =>
                {
                    // Get user info
                    var getUserResult = await this.GetUserByUsername(username);
                    if (!getUserResult.Success) throw new Exception(getUserResult.Message);
                    var userInfo = getUserResult.Model;

                    // Follow user
                    var followResult = await this.instaApi.FollowUserAsync(userInfo.ApiId);
                    if (!followResult.Succeeded)
                    {
                        this.Logger.LogWarning(
                            $"Can't follow [username :{username}]. Error:\t {followResult.Info.Message}");
                    }

                    // TODO: Insert to database
                    this.Logger.LogInformation($"Now you follow: {username} ");
                    await Task.Delay(this.GetDelay());
                    return new OperationResult(true);
                });
        }

        public async Task<OperationResult> FollowUsers(IEnumerable<string> usersnames)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                foreach (string username in usersnames)
                {
                    var followResult = await this.FollowUser(username);
                }

                this.Logger.LogInformation($"Following success!");
                return new OperationResult(true);
            });
        }

        public async Task<OperationResult> UnFollowUser(string username)
        {
            return await this.ProcessRequestAsync(async () =>
                {
                    // Get user info
                    var getUserResult = await this.GetUserByUsername(username);
                    if (!getUserResult.Success) throw new Exception(getUserResult.Message);
                    var userInfo = getUserResult.Model;

                    // Follow user
                    var followResult = await this.instaApi.UnFollowUserAsync(userInfo.ApiId);
                    if (!followResult.Succeeded)
                    {
                        this.Logger.LogWarning(
                            $"Can't unfollow [username :{username}]. Error:\t {followResult.Info.Message}");
                    }

                    // Find Profile in Db
                    var profileRow =
                        await this.Context.InstProfiles.FirstOrDefaultAsync(x => x.ApiId == userInfo.ApiId);
                    if (profileRow == null)
                    {
                        // TODO: To Service or Repository
                        // Add Profile to Db
                        profileRow = this.Mapper.Map<InstProfileRow>(userInfo);
                        this.Context.InstProfiles.Add(profileRow);

                        await this.Context.SaveChangesAsync();

                        profileRow =
                            await this.Context.InstProfiles.FirstOrDefaultAsync(x => x.ApiId == userInfo.ApiId);
                    }

                    // Find Follow request in Db
                    var followRequestRow =
                        this.Context.FollowRequests.FirstOrDefault(f => f.InstProfileId == profileRow.Id);
                    if (followRequestRow == null)
                    {
                        // TODO: To Service or Repository
                        var followRequest = this.Mapper.Map<FollowRequest>(profileRow);
                        followRequest.IsUnfollowed = true;

                        this.Context.FollowRequests.Add(this.Mapper.Map<FollowRequestRow>(followRequest));

                        await this.Context.SaveChangesAsync();
                    }
                    else
                    {
                        followRequestRow.IsUnfollowed = true;

                        await this.Context.SaveChangesAsync();
                    }

                    this.Logger.LogInformation($"Now you unfollow: {username} ");
                    await Task.Delay(this.GetDelay());
                    return new OperationResult(true);
                });
        }

        public async Task<OperationResult> UnFollowUsers(IEnumerable<string> usersnames)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                foreach (string username in usersnames)
                {
                    var followResult = await this.UnFollowUser(username);
                }

                this.Logger.LogInformation($"Unfollowing success!");
                return new OperationResult(true);
            });
        }

        #endregion

        public async Task<OperationResult<List<string>>> GetLikersByUsernameAsync(string username, int postsCount = 1)
        {
            return await this.ProcessRequestAsync(async () =>
            {
                var getUserMediaResult = await this.instaApi.GetUserMediaAsync(username);
                if (!getUserMediaResult.Succeeded)
                {
                    throw new Exception($"Can't get media [username:{username}]. Error:\t{getUserMediaResult.Info.Message }");
                }

                var userMedia = getUserMediaResult.Value;
                if (userMedia.Count < 1)
                {
                    throw new Exception($"User {username} has not posts.");
                }

                var lastMedia = userMedia.OrderByDescending(x => x.DeviceTimeStap /*x.TakenAt*/).FirstOrDefault();

                var getLikersResult = await this.instaApi.GetMediaLikersAsync(lastMedia.InstaIdentifier);
                if (!getLikersResult.Succeeded)
                {
                    throw new Exception($"Can't get likers for media [mediaId :{lastMedia.InstaIdentifier}]. Error:\t {getLikersResult.Info.Message}");
                }

                var likersIds = getLikersResult.Value.Select(x => x.Pk).ToList();

                this.Logger.LogInformation($"Get {likersIds.Count} users, that liked last {username}'s post.");
                return new OperationResult<List<string>>(likersIds);
            });
        }

        public async Task<OperationResult> CleanMyFollowing(int maxPages = 0)
        {
            return await this.ProcessRequestAsync(async () =>
                {
                    var currentFollowersResponse = await this.instaApi.GetCurrentUserFollowersAsync(maxPages);
                    if (!currentFollowersResponse.Succeeded)
                    {
                        return new OperationResult(false, currentFollowersResponse.Info.Message);
                    }

                    this.Context.AppUsers.Add(new AppUserRow()
                                                  {
                                                      Created = DateTime.Now,
                                                      Deleted = DateTime.MinValue,
                                                      Email = "fckd.kiev@gmail.com",
                                                      Password = "fckdhadiach",
                                                      Username = "ADMIN",
                                                      Phone = string.Empty

                                                  });
                    await this.Context.SaveChangesAsync();

                    var followers = currentFollowersResponse.Value;
                    foreach (var follower in followers)
                    {
                        await this.UnFollowUser(follower.UserName);
                    }

                    return new OperationResult(true);
                });
        }

        #region NotImplemented

        public Task<OperationResult<List<string>>> GetCommentersByUsernameAsync(string username, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<List<string>>> GetLikersByHashtagAsync(string hashtag, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<List<string>>> GetLikersByLocationAsync(string location, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<List<string>>> GetCommentersByHashtagAsync(string hashtag, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<List<string>>> GetCommentersByLocationAsync(string location, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<List<string>>> GetHasPostWithHashtagAsync(string hashtag, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<List<string>>> GetHasPostWithLocationAsync(string location, int postsCount = 1)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
