using InstagramTools.Common.Models;

namespace InstagramTools.Common.Interfaces
{
    public interface IInstaToolsService
    {
        OperationResult BuildApiManager(LoginModel loginModel);

        //Get users, whose liked last N photos in xxxx profile
        OperationResult<ProfileModel> GetLikesFromPhotosByPageId(int maxPhoto, int pageId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"> User name</param>
        /// <param name="usersToFollow"> number of users to follow</param>
        /// <returns></returns>
        OperationResult FollowUsersWhichLikeLastPost(string username, int usersToFollow);

    }
}
