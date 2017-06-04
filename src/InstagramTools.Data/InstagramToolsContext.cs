using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramTools.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InstagramTools.Data
{
    public class InstagramToolsContext : DbContext
    {
        public InstagramToolsContext(DbContextOptions<InstagramToolsContext> options)
			: base(options)
		{
        }

        public virtual DbSet<AppUserRow> AppUsers { get; set; }
        public virtual DbSet<InstLoginInfoRow> InstLoginInfo { get; set; }
        public virtual DbSet<InstProfileRow> InstProfiles { get; set; }
        public virtual DbSet<FollowRequestRow> FollowRequests { get; set; }
    }
}
