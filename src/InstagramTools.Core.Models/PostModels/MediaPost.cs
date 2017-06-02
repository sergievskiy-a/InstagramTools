using System;
using System.Collections.Generic;
using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Models.PostModels
{
    public class MediaPost
    {
        public DateTime TakenAt { get; set; }
        public string Pk { get; set; }

        public string InstaIdentifier { get; set; }

        public DateTime DeviceTimeStap { get; set; }
        public MediaPostType MediaType { get; set; }

        public string Code { get; set; }

        public string ClientCacheKey { get; set; }
        public string FilterType { get; set; }


        public List<PostImage> Images { get; set; } = new List<PostImage>();

        public int Width { get; set; }

        public string Height { get; set; }

        public ProfileModel User { get; set; }

        public string TrakingToken { get; set; }

        public int LikesCount { get; set; }

        public string NextMaxId { get; set; }

        public PostCaption Caption { get; set; }

        public string CommentsCount { get; set; }

        public bool PhotoOfYou { get; set; }

        public bool HasLiked { get; set; }

        public List<PostUserTag> Tags { get; set; } = new List<PostUserTag>();

        public List<ProfileModel> Likers { get; set; } = new List<ProfileModel>();
        
        public int ViewCount { get; set; }

        public bool HasAudio { get; set; }
    }
}
