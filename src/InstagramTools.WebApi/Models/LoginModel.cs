namespace InstagramTools.WebApi.Models
{
	public class LoginModel
	{
        public int UserId { get; set; }
        public string Username { get; set; }

		public string Password { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        //TODO: Other environment params
        //OS
        //Device
        //Country
        //IP
	}
}
