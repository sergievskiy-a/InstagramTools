namespace InstagramTools.Core.Models.PostModels
{
    public class PostImage
    {
        public PostImage(string uri, int width, int height)
        {
            URI = uri;
            Width = width;
            Height = height;
        }

        public PostImage()
        {
        }

        public string URI { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
