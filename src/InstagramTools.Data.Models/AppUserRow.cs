using System.Collections.Generic;

namespace InstagramTools.Data.Models
{
    public class AppUserRow : Entity
    {
        public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

        public string RoleId { get; set; }
        public RoleRow Role { get; set; }

        // List of Instagram profiles' credentionals
	    public virtual List<InstLoginInfoRow> InstCredentionals { get; set; }
    }
}
