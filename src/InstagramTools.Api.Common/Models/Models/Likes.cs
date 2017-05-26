namespace InstagramTools.Api.Common.Models.Models
{
    public class Likes
    {
        public int Count { get; set; }
        public InstaUserList VisibleLikedUsers { get; set; }
    }
}