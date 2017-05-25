using System;
using System.Linq;
using InstagramTools.Api.Classes.Models;
using InstagramTools.Api.Classes.ResponseWrappers;

namespace InstagramTools.Api.Converters
{
    internal class InstaMediaListConverter : IObjectConverter<InstaMediaList, InstaMediaListResponse>
    {
        public InstaMediaListResponse SourceObject { get; set; }

        public InstaMediaList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var mediaList = new InstaMediaList();
            mediaList.AddRange(
                SourceObject.Medias.Select(ConvertersFabric.GetSingleMediaConverter)
                    .Select(converter => converter.Convert()));
            mediaList.PageSize = SourceObject.ResultsCount;
            return mediaList;
        }
    }
}