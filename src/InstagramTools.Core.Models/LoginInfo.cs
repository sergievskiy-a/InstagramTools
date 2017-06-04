using System;
using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Models
{
    public class LoginInfo : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Instagram profile of this credentionals
        public string ProfileId { get; set; }
        public InstProfile Profile { get; set; }

        // InstagramTools user
        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; }
    }
}
