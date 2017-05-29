using System.Threading.Tasks;
using InstagramTools.Common.Models;

namespace InstagramTools.Common.Interfaces
{
    public interface IInstaToolsService
    {
        Task<OperationResult> BuildApiManagerAsync(LoginModel loginModel);

        //Get users, whose liked last N photos in xxxx profile
        Task<OperationResult<ProfileModel>> GetLikesFromPhotosByPageIdAsync(int maxPhoto, int pageId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"> User name</param>
        /// <param name="usersToFollow"> number of users to follow</param>
        /// <returns></returns>
        Task<OperationResult> FollowUsersWhichLikeLastPostAsync(string username);

        Task<OperationResult> FollowSubscribersOfUser(string username);

    }
}
