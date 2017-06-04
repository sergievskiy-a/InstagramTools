using System.Collections.Generic;

namespace InstagramTools.Core.Models
{
    public class AppUser : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }

        // List of Instagram profiles' credentionals
        public virtual List<LoginInfo> InstCredentionals { get; set; }
    }
}
