﻿using System.Collections.Generic;

namespace InstagramTools.Api.Classes.Models
{
    public class InstaFeed
    {
        public int MediaItemsCount => Medias.Count;
        public int StoriesItemsCount => Stories.Count;

        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();
        public List<InstaStory> Stories { get; set; } = new List<InstaStory>();

        public int Pages { get; set; } = 0;
    }
}