using System;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaCarouselItemConverter : IObjectConverter<InstaCarouselItem, InstaCarouselItemResponse>
    {
        public InstaCarouselItemResponse SourceObject { get; set; }

        public InstaCarouselItem Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var carouselItem = new InstaCarouselItem
            {
                CarouselParentId = this.SourceObject.CarouselParentId,
                Height = int.Parse(this.SourceObject.Height),
                Width = int.Parse(this.SourceObject.Width)
            };
            foreach (var image in this.SourceObject.Images.Candidates)
                carouselItem.Images.Add(new MediaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
            return carouselItem;
        }
    }
}