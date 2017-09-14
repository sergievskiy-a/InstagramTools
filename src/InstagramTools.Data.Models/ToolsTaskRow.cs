using System;
using System.Collections.Generic;

namespace InstagramTools.Data.Models
{
    public class ToolsTaskRow : Entity
    {
        public Guid InstProfileId { get; set; }// для якого профіля ця таска
        public InstProfileRow InstProfile { get; set; }

        public Guid TaskTypeId { get; set; }
        public ToolsTaskTypeRow TaskTypeRow { get; set; }

        public DateTime Completed { get; set; }// коли закінчена

        public virtual List<MessageRow> Messages { get; set; }
    }
}
