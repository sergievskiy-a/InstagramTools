using System.Collections.Generic;

namespace InstagramTools.Core.Models.TaskModels
{
    public class ToolsTaskType : Entity
    {
        public string Title { get; set; }

        public virtual List<ToolsTask> ToolsTasks { get; set; }
    }
}
