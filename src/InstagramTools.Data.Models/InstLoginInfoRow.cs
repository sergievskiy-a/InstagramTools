﻿using System;

namespace InstagramTools.Data.Models
{
    public class InstLoginInfoRow : Entity
    {
        public string Username { get; set; }
		public string Password { get; set; }

        // Instagram profile of this credentionals
        public string ProfileId { get; set; }
        public InstProfileRow Profile { get; set; }

        // InstagramTools user
        public Guid OwnerId { get; set; }
        public AppUserRow Owner { get; set; }
    }
}
