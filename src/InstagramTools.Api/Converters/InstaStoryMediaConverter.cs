using InstagramTools.Api.Classes.Models;
using InstagramTools.Api.Classes.ResponseWrappers;

namespace InstagramTools.Api.Converters
{
    class InstaStoryMediaConverter : IObjectConverter<InstaStoryMedia, InstaStoryMediaResponse>
    {
        public InstaStoryMediaResponse SourceObject { get; set; }

        public InstaStoryMedia Convert()
        {
            var instaStoryMedia = new InstaStoryMedia
            {
                Media = ConvertersFabric.GetStoryItemConverter(SourceObject.Media).Convert()
            };

            return instaStoryMedia;
        }
    }
}
