using System;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaCarouselConverter : IObjectConverter<InstaCarousel, InstaCarouselResponse>
    {
        public InstaCarouselResponse SourceObject { get; set; }

        public InstaCarousel Convert()
        {
            var carousel = new InstaCarousel();
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            foreach (var item in SourceObject)
            {
                var carouselItem = ConvertersFabric.GetCarouselItemConverter(item);
                carousel.Add(carouselItem.Convert());
            }
            return carousel;
        }
    }
}