using System;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaUserTagConverter : IObjectConverter<InstaUserTag, InstaUserTagResponse>
    {
        public InstaUserTagResponse SourceObject { get; set; }

        public InstaUserTag Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var userTag = new InstaUserTag();
            if (this.SourceObject.Position?.Length == 2)
                userTag.Position = new InstaPosition(this.SourceObject.Position[0], this.SourceObject.Position[1]);
            userTag.TimeInVideo = this.SourceObject.TimeInVideo;
            if (this.SourceObject.User != null)
                userTag.User = ConvertersFabric.GetUserConverter(this.SourceObject.User).Convert();
            return userTag;
        }
    }
}