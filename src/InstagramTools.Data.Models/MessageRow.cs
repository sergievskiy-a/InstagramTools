using System;

namespace InstagramTools.Data.Models
{
    public class MessageRow : Entity
    {
        public Guid TaskId { get; set; }
        public TaskRow Task { get; set; } // для якої таски повідомлення

        public Guid MessageTypeId { get; set; }
        public MessageTypeRow MessageType { get; set; }

        public string Message { get; set; }

        public DateTime Opened { get; set; }
    }
}
