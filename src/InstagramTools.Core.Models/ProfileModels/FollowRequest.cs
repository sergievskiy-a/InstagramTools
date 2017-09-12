using System;

namespace InstagramTools.Core.Models.ProfileModels
{
    public class FollowRequest : Entity
    {
        public int InstProfileId { get; set; }
        public InstProfile InstProfile { get; set; }

        public bool IsUnfollowed { get; set; }
    }
}
