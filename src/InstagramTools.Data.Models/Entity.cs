using System;

namespace InstagramTools.Data.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedDateTime = DateTime.Now;
        }
    }
}
