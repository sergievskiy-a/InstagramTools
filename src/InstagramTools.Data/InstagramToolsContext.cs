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
        public virtual DbSet<RoleRow> Roles { get; set; }
        public virtual DbSet<InstLoginInfoRow> InstLoginInfo { get; set; }
        public virtual DbSet<InstProfileRow> InstProfiles { get; set; }
        public virtual DbSet<FollowRequestRow> FollowRequests { get; set; }
        public virtual DbSet<MessageRow> Messages { get; set; }
        public virtual DbSet<MessageTypeRow> MessageTypes { get; set; }
        public virtual DbSet<ToolsTaskRow> ToolsTasks { get; set; }
        public virtual DbSet<ToolsTaskTypeRow> ToolsTaskTypes { get; set; }
    }
}