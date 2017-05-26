using InstagramTools.Api.Common.Models.Models;

namespace InstagramTools.Api.Common.Models
{
    public class UserSessionData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public InstaUser LoggedInUder { get; set; }

        public string RankToken { get; set; }
        public string CsrfToken { get; set; }
    }
}