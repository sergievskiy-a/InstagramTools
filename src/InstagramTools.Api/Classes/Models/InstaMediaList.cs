using System.Collections.Generic;

namespace InstagramTools.Api.Classes.Models
{
    public class InstaMediaList : List<InstaMedia>
    {
        public int Pages { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}