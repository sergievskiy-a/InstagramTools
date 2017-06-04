using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramTools.Data.Models
{
    public class FollowRequestRow: Entity
    {
        public Guid InstProfileId { get; set; }
        public InstProfileRow InstProfile { get; set; }

        public string ApiUserId { get; set; }
        public string ApiUserUsername { get; set; }

        public bool IsUnfollowed { get; set; }
    }
}
