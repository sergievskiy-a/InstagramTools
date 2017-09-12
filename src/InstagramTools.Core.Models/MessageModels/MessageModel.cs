using System;

using InstagramTools.Core.Models.TaskModels;

namespace InstagramTools.Core.Models.MessageModels
{
    public class MessageModel : Entity
    {
        public Guid TaskId { get; set; }
        public ToolsTask Task { get; set; } // для якої таски повідомлення

        public Guid MessageTypeId { get; set; }
        public MessageType MessageType { get; set; }

        public string Message { get; set; }

        public DateTime Opened { get; set; }
    }
}
