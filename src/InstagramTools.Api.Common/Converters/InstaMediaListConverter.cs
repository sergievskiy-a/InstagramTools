using System;
using System.Linq;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaMediaListConverter : IObjectConverter<InstaMediaList, InstaMediaListResponse>
    {
        public InstaMediaListResponse SourceObject { get; set; }

        public InstaMediaList Convert()
        {
            if (this.SourceObject == null) throw new ArgumentNullException($"Source object");
            var mediaList = new InstaMediaList();
            mediaList.AddRange(
                this.SourceObject.Medias.Select(ConvertersFabric.GetSingleMediaConverter)
                    .Select(converter => converter.Convert()));
            mediaList.PageSize = this.SourceObject.ResultsCount;
            return mediaList;
        }
    }
}