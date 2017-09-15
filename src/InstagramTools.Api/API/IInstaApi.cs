﻿using System.Threading.Tasks;
using InstagramTools.Api.Common.Models;
using InstagramTools.Api.Common.Models.Models;

namespace InstagramTools.Api.API
{
    public interface IInstaApi
    {
        #region Properties

        /// <summary>
        ///     Indicates whether user authenticated or not
        /// </summary>
        bool IsUserAuthenticated { get; }

        #endregion

        #region Sync Members

        /// <summary>
        ///     Login using given credentials
        /// </summary>
        /// <returns>True is succeed</returns>
        IResult<bool> Login();

        /// <summary>
        ///     Logout from instagram
        /// </summary>
        /// <returns>True if completed without errors</returns>
        IResult<bool> Logout();

        /// <summary>
        ///     Get user timeline feed
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        IResult<InstaFeed> GetUserTimelineFeed(int maxPages = 0);

        /// <summary>
        ///     Get user explore feed
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns><see cref="InstaFeed" />></returns>
        IResult<InstaFeed> GetExploreFeed(int maxPages = 0);

        /// <summary>
        ///     Get all user media by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        IResult<InstaMediaList> GetUserMedia(string username, int maxPages = 0);

        /// <summary>
        ///     Get media by its id (code)
        /// </summary>
        /// <param name="mediaId">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        IResult<InstaMedia> GetMediaById(string mediaId);

        /// <summary>
        ///     Get user info by its user name
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        IResult<InstaUser> GetUser(string username);

        /// <summary>
        ///     Get currently logged in user info
        /// </summary>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        IResult<InstaUser> GetCurrentUser();

        /// <summary>
        ///     Get tag feed by tag value
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        IResult<InstaFeed> GetTagFeed(string tag, int maxPages = 0);

        /// <summary>
        ///     Get followers list by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        IResult<InstaUserList> GetUserFollowers(string username, int maxPages = 0);

        /// <summary>
        ///     Get followers list for currently logged in user
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        IResult<InstaUserList> GetCurentUserFollowers(int maxPages = 0);

        /// <summary>
        ///     Get user tags by username
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        IResult<InstaMediaList> GetUserTags(string username, int maxPages = 0);


        /// <summary>
        ///     Get direct inbox threads for current user
        /// </summary>
        /// <returns>
        ///     <see cref="InstaDirectInboxContainer" />
        /// </returns>
        IResult<InstaDirectInboxContainer> GetDirectInbox();

        /// <summary>
        ///     Get direct inbox thread by its id
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        IResult<InstaDirectInboxThread> GetDirectInboxThread(string threadId);

        /// <summary>
        ///     Get recent recipients (threads and users)
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        IResult<InstaRecipients> GetRecentRecipients();

        /// <summary>
        ///     Get ranked recipients (threads and users)
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        IResult<InstaRecipients> GetRankedRecipients();

        /// <summary>
        ///     Get recent activity info
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        IResult<InstaActivityFeed> GetRecentActivity(int maxPages = 0);

        /// <summary>
        ///     Get activity of following
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        IResult<InstaActivityFeed> GetFollowingRecentActivity(int maxPages = 0);

        /// <summary>
        ///     Like instagram media by id
        /// </summary>
        /// <param name="mediaId">Media Id</param>
        /// <returns>True if success</returns>
        IResult<bool> LikeMedia(string mediaId);

        /// <summary>
        ///     Like instagram media by id
        /// </summary>
        /// <param name="mediaId">Media Id</param>
        /// <returns>True if success</returns>
        IResult<bool> UnlikeMedia(string mediaId);

        /// <summary>
        ///     Follow user by its by id
        /// </summary>
        /// <param name="userId">User Id <see cref="InstaUser.Pk" /></param>
        /// <returns>True if success</returns>
        IResult<InstaFriendshipStatus> FollowUser(string userId);

        /// <summary>
        ///     Stop follow user by its by id
        /// </summary>
        /// <param name="userId">User Id <see cref="InstaUser.Pk" /></param>
        /// <returns>True if success</returns>
        IResult<InstaFriendshipStatus> UnFollowUser(string userId);

        /// <summary>
        ///     Set current account private
        /// </summary>
        IResult<InstaUser> SetAccountPrivate();

        /// <summary>
        ///     Set current account public
        /// </summary>
        IResult<InstaUser> SetAccountPublic();

        /// <summary>
        ///     Comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        IResult<InstaComment> CommentMedia(string mediaId, string text);

        /// <summary>
        ///     Delete comment from media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        IResult<bool> DeleteComment(string mediaId, string commentId);

        /// <summary>
        ///     Uploads photo
        /// </summary>
        /// <param name="image">Photo</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        IResult<InstaMedia> UploadPhoto(MediaImage image, string caption);

        /// <summary>
        ///     Configures photo
        /// </summary>
        /// <param name="image">Photo</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        IResult<InstaMedia> ConfigurePhoto(MediaImage image, string uploadId, string caption);

        /// <summary>
        ///     Get user's Story Tray
        /// </summary>
        IResult<InstaStoryTray> GetStoryTray();

        /// <summary>
        ///     Get the story by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        IResult<InstaStory> GetUserStory(long userId);

        /// <summary>
        ///     Upload story photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        IResult<InstaStoryMedia> UploadStoryPhoto(MediaImage image, string caption);

        /// <summary>
        ///     Configure story photo
        /// </summary>
        /// <param name="image">Photo to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        IResult<InstaStoryMedia> ConfigureStoryPhoto(MediaImage image, string uploadId, string caption);

        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">The new password (shouldn't be the same old password, and should be a password you never used here)</param>
        /// <returns>Return true if the password is changed</returns>
        IResult<bool> ChangePassword(string oldPassword, string newPassword);

        /// <summary>
        ///     Delete a media (photo or video)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="mediaType">The type of the media</param>
        /// <returns>Return true if the media is deleted</returns>
        IResult<bool> DeleteMedia(string mediaId, InstaMediaType mediaType);

        #endregion

        #region Async Members

        /// <summary>
        ///     Login using given credentials asynchronously
        /// </summary>
        /// <returns>True is succeed</returns>
        Task<IResult<bool>> LoginAsync();

        /// <summary>
        ///     Logout from instagram asynchronously
        /// </summary>
        /// <returns>True if completed without errors</returns>
        Task<IResult<bool>> LogoutAsync();

        /// <summary>
        ///     Get user timeline feed asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        Task<IResult<InstaFeed>> GetUserTimelineFeedAsync(int maxPages = 0);

        /// <summary>
        ///     Get user explore feed asynchronously (Explore tab info)
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns><see cref="InstaFeed" />></returns>
        Task<IResult<InstaFeed>> GetExploreFeedAsync(int maxPages = 0);

        /// <summary>
        ///     Get all user media by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserMediaAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get media by its id asynchronously
        /// </summary>
        /// <param name="mediaId">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId);

        /// <summary>
        ///     Get user info by its user name asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        Task<IResult<InstaUser>> GetUserAsync(string username);

        /// <summary>
        ///     Get currently logged in user info asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaUser" />
        /// </returns>
        Task<IResult<InstaUser>> GetCurrentUserAsync();

        /// <summary>
        ///     Get tag feed by tag value asynchronously
        /// </summary>
        /// <param name="tag">Tag value</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaFeed" />
        /// </returns>
        Task<IResult<InstaFeed>> GetTagFeedAsync(string tag, int maxPages = 0);

        /// <summary>
        ///     Get followers list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        Task<IResult<InstaUserList>> GetUserFollowersAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get followers list by current user asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        Task<IResult<InstaUserList>> GetCurrentUserFollowersAsync(int maxPages = 0);

        /// <summary>
        ///     Get followings list by username asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        Task<IResult<InstaUserList>> GetUserFollowingsAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get following list for currently logged in user asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaUserList" />
        /// </returns>
        Task<IResult<InstaUserList>> GetCurrentUserFollowingsAsync(int maxPages = 0);

        /// <summary>
        ///     Get user tags by username asynchronously
        ///     <remarks>Returns media list containing tags</remarks>
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetUserTagsAsync(string username, int maxPages = 0);

        /// <summary>
        ///     Get direct inbox threads for current user asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaDirectInboxContainer" />
        /// </returns>
        Task<IResult<InstaDirectInboxContainer>> GetDirectInboxAsync();

        /// <summary>
        ///     Get direct inbox thread by its id asynchronously
        /// </summary>
        /// <param name="threadId">Thread id</param>
        /// <returns>
        ///     <see cref="InstaDirectInboxThread" />
        /// </returns>
        Task<IResult<InstaDirectInboxThread>> GetDirectInboxThreadAsync(string threadId);

        /// <summary>
        ///     Get recent recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRecentRecipientsAsync();

        /// <summary>
        ///     Get ranked recipients (threads and users) asynchronously
        /// </summary>
        /// <returns>
        ///     <see cref="InstaRecipients" />
        /// </returns>
        Task<IResult<InstaRecipients>> GetRankedRecipientsAsync();

        /// <summary>
        ///     Get recent activity info asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetRecentActivityAsync(int maxPages = 0);

        /// <summary>
        ///     Get activity of following asynchronously
        /// </summary>
        /// <param name="maxPages">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaActivityFeed" />
        /// </returns>
        Task<IResult<InstaActivityFeed>> GetFollowingRecentActivityAsync(int maxPages = 0);

        /// <summary>
        ///     Like media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> LikeMediaAsync(string mediaId);

        /// <summary>
        ///     Remove like from media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> UnLikeMediaAsync(string mediaId);

        /// <summary>
        ///     Follow user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipStatus>> FollowUserAsync(string userId);

        /// <summary>
        ///     Stop follow user
        /// </summary>
        /// <param name="userId">User id</param>
        Task<IResult<InstaFriendshipStatus>> UnFollowUserAsync(string userId);

        /// <summary>
        ///     Get media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<InstaCommentList>> GetMediaCommentsAsync(string mediaId, int maxPages = 0);

        /// <summary>
        ///     Get media comments
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<InstaUserList>> GetMediaLikersAsync(string mediaId);

        /// <summary>
        ///     Set current account private
        /// </summary>
        Task<IResult<InstaUser>> SetAccountPrivateAsync();

        /// <summary>
        ///     Set current account public
        /// </summary>
        Task<IResult<InstaUser>> SetAccountPublicAsync();

        /// <summary>
        ///     Comment media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        Task<IResult<InstaComment>> CommentMediaAsync(string mediaId, string text);

        /// <summary>
        ///     Delete comment from media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        /// <param name="commentId">Comment id</param>
        Task<IResult<bool>> DeleteCommentAsync(string mediaId, string commentId);

        /// <summary>
        ///     Upload photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadPhotoAsync(MediaImage image, string caption);

        /// <summary>
        ///     Configure photo
        /// </summary>
        /// <param name="image">Photo to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        Task<IResult<InstaMedia>> ConfigurePhotoAsync(MediaImage image, string uploadId, string caption);

        /// <summary>
        ///     Get user's Story Tray
        /// </summary>
        Task<IResult<InstaStoryTray>> GetStoryTrayAsync();

        /// <summary>
        ///     Get the story by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        Task<IResult<InstaStory>> GetUserStoryAsync(long userId);

        /// <summary>
        ///     Upload story photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(MediaImage image, string caption);

        /// <summary>
        ///     Configure story photo
        /// </summary>
        /// <param name="image">Photo to configure</param>
        /// <param name="uploadId">Upload id</param>
        /// <param name="caption">Caption</param>
        /// <returns></returns>
        Task<IResult<InstaStoryMedia>> ConfigureStoryPhotoAsync(MediaImage image, string uploadId, string caption);

        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">The new password (shouldn't be the same old password, and should be a password you never used here)</param>
        /// <returns>Return true if the password is changed</returns>
        Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword);

        /// <summary>
        ///     Delete a media (photo or video)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="mediaType">The type of the media</param>
        /// <returns>Return true if the media is deleted</returns>
        Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType);

        #endregion
    }
}