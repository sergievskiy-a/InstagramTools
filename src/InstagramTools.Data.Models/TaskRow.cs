using System;

namespace InstagramTools.Data.Models
{
    public class TaskRow : Entity
    {
        public Guid InstProfileId { get; set; }//для якого профіля ця таска
        public InstProfileRow InstProfile { get; set; }

        public Guid TaskTypeId { get; set; }
        public TaskTypeRow TaskTypeRow { get; set; }

        public DateTime Completed { get; set; }//коли закінчена
    }
}
