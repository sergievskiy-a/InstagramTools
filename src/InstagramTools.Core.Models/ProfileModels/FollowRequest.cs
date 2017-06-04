using System;

namespace InstagramTools.Core.Models.ProfileModels
{
    public class FollowRequest : Entity
    {
        public Guid InstProfileId { get; set; }
        public InstProfile InstProfile { get; set; }

        public string ApiUserId { get; set; }
        public string ApiUserUsername { get; set; }

        public bool IsUnfollowed { get; set; }
    }
}
