using System;
using System.Collections.Generic;

namespace InstagramTools.Core.Models
{
    public class Role
    {
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }

        public List<AppUser> Users { get; set; }
    }
}
