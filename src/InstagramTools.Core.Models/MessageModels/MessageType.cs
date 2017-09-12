using System.Collections.Generic;

namespace InstagramTools.Core.Models.MessageModels
{
    public class MessageType : Entity
    {
        public string Title { get; set; }

        public virtual List<MessageModel> Messages { get; set; }
    }
}
