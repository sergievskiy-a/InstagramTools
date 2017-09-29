using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private readonly IInstaApiBuilder apiBuilder;
        private IInstaApi instaApi;
        private readonly TasksMonitor _monitor;

        //KOSTIL'
        private static string userName;
        private static string password;

        #region Constructors

        public InstaToolsService(IConfigurationRoot root, ILogger<InstaToolsService> logger,
            IMemoryCache memoryCache, IMapper mapper, InstagramToolsContext context, IInstaApiBuilder apiBuilder, TasksMonitor monitor)
            : base(root, logger, memoryCache, mapper, context)
        {
            this.apiBuilder = apiBuilder;
            _monitor = monitor;
            _monitor.AddTask("CleanMyFollowing");
            _monitor.AddTask("FollowUsersWhichLikeLastPostAsyncTask");
            // TODO: REPLACE TASK NAMES TO CONSTANT SERVICE
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
                userName = loginModel.Username;
                password = loginModel.Password;
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
                    var getUserResult = await this.GetProfileByUsername(username);
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
                    var getUserResult = await this.GetProfileByUsername(username);
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

                var likersIds = getLikersResult.Value.Select(x => x.UserName).ToList();

                this.Logger.LogInformation($"Get {likersIds.Count} users, that liked last {username}'s post.");
                return new OperationResult<List<string>>(likersIds);
            });
        }

        public async Task<OperationResult> CleanMyFollowing(int maxPages = 0)
        {
            return await this.ProcessRequestAsync(async () =>
                {
                    var currentFollowingsResponse = await this.instaApi.GetCurrentUserFollowingsAsync(maxPages);
                    if (!currentFollowingsResponse.Succeeded)
                    {
                        return new OperationResult(false, currentFollowingsResponse.Info.Message);
                    }

                    Context.AppUsers.Add(new AppUserRow()
                    {
                        Created = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        Email = "fckd.kiev@gmail.com",
                        Password = "fckdhadiach",
                        Username = "ADMIN",
                        Phone = string.Empty

                    });
                    await this.Context.SaveChangesAsync();

                    var followings = currentFollowingsResponse.Value;
                    foreach (var following in followings)
                    {
                        await this.UnFollowUser(following.UserName);
                    }

                    return new OperationResult(true);
                });
        }

        public async Task<OperationResult> CleanMyFollowing(CancellationToken ct, int maxPages = 0)
        {
            this.instaApi = this.apiBuilder.SetUser(new UserSessionData
            {
                UserName = userName,
                Password = password
            }).Build();

            var logInResult = await this.instaApi.LoginAsync();
            //Kostil'

            var currentFollowingsResponse = await this.instaApi.GetCurrentUserFollowingsAsync(maxPages);
            if (!currentFollowingsResponse.Succeeded)
            {
                return new OperationResult(false, currentFollowingsResponse.Info.Message);
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

            var followings = currentFollowingsResponse.Value;
            foreach (var following in followings)
            {
                if (ct.IsCancellationRequested)
                {
                    Logger.LogTrace("TASK CANCELED");
                    return new OperationResult(true);
                }
                await this.UnFollowUser(following.UserName);
            }

            return new OperationResult(true);
        }

        public async Task<OperationResult> FollowUsersWhichLikeLastPostAsyncTask(string username, CancellationToken cancellationToken)
        {
            this.instaApi = this.apiBuilder.SetUser(new UserSessionData
            {
                UserName = userName,
                Password = password
            }).Build();

            var logInResult = await this.instaApi.LoginAsync();
            //Kostil'
            var result = await GetLikersByUsernameAsync(username);
            if(!result.Success) 
                throw new Exception();

            var users = result.Model;
            var dbUsers = Context.InstProfiles.AsQueryable();
            foreach (var user in users)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return new OperationResult(true, "Canceled");
                }
                if (await dbUsers.AnyAsync(x => x.UserName == user)) continue;

                var followResult = await FollowUser(user);
                if (!followResult.Success)
                {
                    return new OperationResult(false);
                }
            }

            return new OperationResult(true, "Done");


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
