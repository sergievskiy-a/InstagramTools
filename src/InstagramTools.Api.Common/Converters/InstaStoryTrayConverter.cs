using System;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaStoryTrayConverter : IObjectConverter<InstaStoryTray, InstaStoryTrayResponse>
    {
        public InstaStoryTrayResponse SourceObject { get; set; }
        
        public InstaStoryTray Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");

            var storyTray = new InstaStoryTray {
                Status = this.SourceObject.Status,
                StickerVersion = this.SourceObject.StickerVersion,
                StoryRankingToken = this.SourceObject.StoryRankingToken
            };

            if (this.SourceObject.Tray.Count > 0)
                foreach (var story in this.SourceObject.Tray)
                    storyTray.Tray.Add(ConvertersFabric.GetStoryConverter(story).Convert());

            return storyTray;
        }
    }
}