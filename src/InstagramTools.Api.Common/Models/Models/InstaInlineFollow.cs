namespace InstagramTools.Api.Common.Models.Models
{
    public class InstaInlineFollow
    {
        public bool IsOutgoingRequest { get; set; }
        public bool IsFollowing { get; set; }
        public InstaUser User { get; set; }
    }
}