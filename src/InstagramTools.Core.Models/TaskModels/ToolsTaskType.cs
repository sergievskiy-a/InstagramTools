using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramTools.Core.Models.TaskModels
{
    public class ToolsTaskType : Entity
    {
        public string Title { get; set; }

        public virtual List<ToolsTask> ToolsTasks { get; set; }
    }
}
