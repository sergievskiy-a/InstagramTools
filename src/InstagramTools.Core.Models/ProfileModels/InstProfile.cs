namespace InstagramTools.Core.Models.ProfileModels
{
    public class InstProfile : Entity
    {
        public string UserName { get; set; }
        public bool HasAnonymousProfilePicture { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }

        public int UnseenCount { get; set; }
        public string ProfilePicture { get; set; }

        public string ProfilePictureId { get; set; }

        public string FullName { get; set; }

        public long InstaIdentifier { get; set; }

        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }

        public int FollowedByCount { get; set; }
        public int FollowerCount { get; set; }

        public string ApiId { get; set; }

        public string MutualFollowersCount { get; set; }
        public static InstProfile Empty => new InstProfile { FullName = string.Empty, UserName = string.Empty };
    }
}
