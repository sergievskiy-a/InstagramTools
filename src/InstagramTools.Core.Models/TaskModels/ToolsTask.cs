using System;
using System.Collections.Generic;

using InstagramTools.Core.Models.MessageModels;
using InstagramTools.Core.Models.ProfileModels;

namespace InstagramTools.Core.Models.TaskModels
{
    public class ToolsTask : Entity
    {
        public Guid InstProfileId { get; set; }//для якого профіля ця таска
        public InstProfile InstProfile { get; set; }

        public Guid TaskTypeId { get; set; }
        public ToolsTaskType TaskTypeRow { get; set; }

        public DateTime Completed { get; set; }//коли закінчена

        public virtual List<MessageModel> Messages { get; set; }
    }
}
