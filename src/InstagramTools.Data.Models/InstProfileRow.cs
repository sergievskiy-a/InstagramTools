namespace InstagramTools.Data.Models
{
    public class InstProfileRow : Entity
    {
        // Pk value
        public string ApiId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public long InstaIdentifier { get; set; }
    }
}
