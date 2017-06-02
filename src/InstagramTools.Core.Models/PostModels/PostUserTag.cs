using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Models.PostModels
{
    public class PostUserTag
    {
        public Position Position { get; set; }

        public string TimeInVideo { get; set; }

        public ProfileModel User { get; set; }
    }
}
