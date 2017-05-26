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
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var userTag = new InstaUserTag();
            if (SourceObject.Position?.Length == 2)
                userTag.Position = new InstaPosition(SourceObject.Position[0], SourceObject.Position[1]);
            userTag.TimeInVideo = SourceObject.TimeInVideo;
            if (SourceObject.User != null)
                userTag.User = ConvertersFabric.GetUserConverter(SourceObject.User).Convert();
            return userTag;
        }
    }
}