using System.Collections.Generic;

namespace InstagramTools.Data.Models
{
    public class AppUserRow : Entity
    {
        public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

        // List of Instagram profiles' credentionals
	    public virtual List<InstLoginInfo> InstCredentionals { get; set; }
    }
}
