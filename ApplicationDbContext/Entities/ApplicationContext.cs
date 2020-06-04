using System.Data.Entity;

namespace TalanDemoDbContext.Entities
{
	public class ApplicationContext : DbContext
	{
		public DbSet<CivilState> CivilStates { get; set; }
	}
}