using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstagramTools.Data.Models
{
    public class RoleRow
    {
        [Key]
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }

        public List<AppUserRow> Users { get; set; }
    }
}
