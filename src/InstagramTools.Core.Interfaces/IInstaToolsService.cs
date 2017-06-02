using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramTools.Common;
using InstagramTools.Common.Models;
using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Interfaces
{
    public interface IInstaToolsService
    {

        #region Main

        Task<OperationResult> BuildApiManagerAsync(LoginModel loginModel);

        Task<OperationResult<ProfileModel>> GetUserByUsername(string username);
        Task<OperationResult<List<ProfileModel>>> GetMyFollowers(int maxPages = 50);
        Task<OperationResult<List<ProfileModel>>> GetUserFollowers(string username, int maxPages = 50);
        Task<OperationResult> FollowUser(string username);
        Task<OperationResult> FollowUsers(IEnumerable<string> usersnames);
        Task<OperationResult> UnFollowUser(string username);
        Task<OperationResult> UnFollowUsers(IEnumerable<string> usersnames);

        #endregion


        Task<OperationResult> FollowUsersWhichLikeLastPostAsync(string username);
        Task<OperationResult> FollowSubscribersOfUser(string username);
        Task<OperationResult> UnfollowUnreciprocalUsers();

    }
}
