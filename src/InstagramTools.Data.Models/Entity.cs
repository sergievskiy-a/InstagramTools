﻿using System;

namespace InstagramTools.Data.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }
    }
}
