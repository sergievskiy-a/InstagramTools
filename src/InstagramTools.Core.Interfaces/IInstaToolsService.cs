using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;
using InstagramTools.Common.Models;
using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Interfaces
{
    public interface IInstaToolsService
    {

        #region Main

        Task<OperationResult> BuildApiManagerAsync(LoginModel loginModel);

        Task<OperationResult<InstProfile>> GetProfileByUsername(string username);

        Task<OperationResult<List<InstProfile>>> GetMyFollowers(int maxPages = 50);
        Task<OperationResult<List<InstProfile>>> GetFollowersByUsername(string username, int maxPages = 50);

        Task<OperationResult> FollowUser(string username);
        Task<OperationResult> FollowUsers(IEnumerable<string> usersnames);

        Task<OperationResult> UnFollowUser(string username);
        Task<OperationResult> UnFollowUsers(IEnumerable<string> usersnames);

        #endregion

        Task<OperationResult<List<string>>> GetLikersByUsernameAsync(string username, int postsCount);
        Task<OperationResult<List<string>>> GetCommentersByUsernameAsync(string username, int postsCount);

        Task<OperationResult<List<string>>> GetLikersByHashtagAsync(string hashtag, int postsCount);
        Task<OperationResult<List<string>>> GetLikersByLocationAsync(string location, int postsCount);

        Task<OperationResult<List<string>>> GetCommentersByHashtagAsync(string hashtag, int postsCount);
        Task<OperationResult<List<string>>> GetCommentersByLocationAsync(string location, int postsCount);

        Task<OperationResult<List<string>>> GetHasPostWithHashtagAsync(string hashtag, int postsCount);
        Task<OperationResult<List<string>>> GetHasPostWithLocationAsync(string location, int postsCount);

        Task<OperationResult> CleanMyFollowing(int maxPages = 0);
        Task<OperationResult> CleanMyFollowing(CancellationToken ct, int maxPages = 0);
    }
}
