﻿using System;

namespace InstagramTools.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
    }
}