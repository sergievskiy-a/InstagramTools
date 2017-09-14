namespace InstagramTools.Api.Common.Models.Models
{
    public class InstaPosition
    {
        public InstaPosition(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}