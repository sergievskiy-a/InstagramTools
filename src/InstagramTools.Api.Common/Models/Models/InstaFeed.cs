using System.Collections.Generic;

namespace InstagramTools.Api.Common.Models.Models
{
    public class InstaFeed
    {
        public int MediaItemsCount => this.Medias.Count;
        public int StoriesItemsCount => this.Stories.Count;

        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();
        public List<InstaStory> Stories { get; set; } = new List<InstaStory>();

        public int Pages { get; set; } = 0;
    }
}