using System;

namespace InstagramTools.Data.Models
{
    public class FollowRequestRow: Entity
    {
        public int InstProfileId { get; set; }
        public InstProfileRow InstProfile { get; set; }

        public bool IsUnfollowed { get; set; }
    }
}
