using System;

namespace InstagramTools.Data.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }

        protected Entity()
        {
            this.Created = DateTime.Now;
        }
    }
}
