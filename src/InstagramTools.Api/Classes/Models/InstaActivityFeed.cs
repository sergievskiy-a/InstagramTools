﻿using System.Collections.Generic;

namespace InstagramTools.Api.Classes.Models
{
    public class InstaActivityFeed
    {
        public bool IsOwnActivity { get; set; } = false;
        public List<InstaRecentActivityFeed> Items { get; set; } = new List<InstaRecentActivityFeed>();
    }
}