namespace InstagramTools.Api.Common.Models.Models
{
    public class MediaImage
    {
        public MediaImage(string uri, int width, int height)
        {
            this.URI = uri;
            this.Width = width;
            this.Height = height;
        }

        public MediaImage()
        {
        }

        public string URI { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}