﻿using System;

namespace InstagramTools.Data.Models
{
    public class FollowRequestRow: Entity
    {
        public Guid InstProfileId { get; set; }
        public InstProfileRow InstProfile { get; set; }

        public string ApiUserId { get; set; }
        public string ApiUserUsername { get; set; }

        public bool IsUnfollowed { get; set; }
    }
}
