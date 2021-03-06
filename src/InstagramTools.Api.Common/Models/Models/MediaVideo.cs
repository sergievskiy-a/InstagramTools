﻿namespace InstagramTools.Api.Common.Models.Models
{
    public class MediaVideo
    {

        public MediaVideo(string url, string width, string height, int type)
        {
            this.Url = url;
            this.Width = width;
            this.Height = height;
            this.Type = type;
        }

        public string Url { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public int Type { get; set; }

    }
}