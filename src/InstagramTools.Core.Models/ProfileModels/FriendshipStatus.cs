namespace InstagramTools.Core.Models.ProfileModels
{
    public class FriendshipStatus
    {
        public bool Following { get; set; }
        public bool IsPrivate { get; set; }
        public bool FollowedBy { get; set; }
        public bool Blocking { get; set; }
        public bool IncomingRequest { get; set; }
        public bool OutgoingRequest { get; set; }
    }
}
