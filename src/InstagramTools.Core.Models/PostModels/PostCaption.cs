using System;
using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Models.PostModels
{
    public class PostCaption
    {
        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }

        public ProfileModel User { get; set; }

        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }
    }
}
