using Microsoft.EntityFrameworkCore;

namespace QueuedDataAccess.Data
{
	public class QueProjectDbContext : DbContext
	{
		public QueProjectDbContext(DbContextOptions<QueProjectDbContext> options) : base(options)
		{
		}
		public virtual DbSet<Activity> Activity { get; set; }
	}
}