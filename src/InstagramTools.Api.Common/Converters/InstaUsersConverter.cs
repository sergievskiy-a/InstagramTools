using System;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaUsersConverter : IObjectConverter<InstaUser, InstaUserResponse>
    {
        public InstaUserResponse SourceObject { get; set; }

        public InstaUser Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var user = new InstaUser
            {
                InstaIdentifier = this.SourceObject.Id
            };
            if (!string.IsNullOrEmpty(this.SourceObject.FullName)) user.FullName = this.SourceObject.FullName;
            if (!string.IsNullOrEmpty(this.SourceObject.ProfilePicture)) user.ProfilePicture = this.SourceObject.ProfilePicture;
            if (!string.IsNullOrEmpty(this.SourceObject.UserName)) user.UserName = this.SourceObject.UserName;
            if (!string.IsNullOrEmpty(this.SourceObject.Pk)) user.Pk = this.SourceObject.Pk;
            if (this.SourceObject.Friendship != null)
                user.FriendshipStatus = ConvertersFabric.GetFriendShipStatusConverter(this.SourceObject.Friendship)
                    .Convert();
            user.HasAnonymousProfilePicture = this.SourceObject.HasAnonymousProfilePicture;
            user.ProfilePictureId = this.SourceObject.ProfilePictureId;
            user.IsVerified = this.SourceObject.IsVerified;
            user.IsPrivate = this.SourceObject.IsPrivate;
            user.UnseenCount = this.SourceObject.UnseenCount;
            user.MutualFollowersCount = this.SourceObject.MutualFollowersCount;
            return user;
        }
    }
}