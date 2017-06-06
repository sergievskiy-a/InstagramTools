using System.Collections.Generic;

namespace InstagramTools.Data.Models
{
    public class ToolsTaskTypeRow : Entity
    {
        public string Title { get; set; }

        public virtual List<ToolsTaskRow> ToolsTasks { get; set; }
    }
}
