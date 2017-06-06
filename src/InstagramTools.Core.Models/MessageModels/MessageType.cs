using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramTools.Core.Models.MessageModels
{
    public class MessageType : Entity
    {
        public string Title { get; set; }

        public virtual List<MessageModel> Messages { get; set; }
    }
}
