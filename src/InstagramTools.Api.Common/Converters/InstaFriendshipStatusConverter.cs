using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaFriendshipStatusConverter :
        IObjectConverter<InstaFriendshipStatus, InstaFriendshipStatusResponse>
    {
        public InstaFriendshipStatusResponse SourceObject { get; set; }

        public InstaFriendshipStatus Convert()
        {
            var friendShip = new InstaFriendshipStatus
            {
                Following = this.SourceObject.Following,
                Blocking = this.SourceObject.Blocking,
                FollowedBy = this.SourceObject.FollowedBy,
                IncomingRequest = this.SourceObject.IncomingRequest,
                OutgoingRequest = this.SourceObject.OutgoingRequest
            };
            friendShip.IncomingRequest = this.SourceObject.IncomingRequest;
            friendShip.IsPrivate = this.SourceObject.IsPrivate;
            return friendShip;
        }
    }
}