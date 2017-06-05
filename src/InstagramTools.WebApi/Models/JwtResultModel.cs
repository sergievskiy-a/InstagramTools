namespace InstagramTools.WebApi.Models
{
    public class JwtResultModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
