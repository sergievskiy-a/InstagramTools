﻿using System.Collections.Generic;

namespace InstagramTools.Data.Models
{
    public class MessageTypeRow : Entity
    {
        public string Title { get; set; }

        public virtual List<MessageRow> Messages { get; set; }
    }
}
