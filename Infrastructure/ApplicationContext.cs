using System.Data.Entity;
using Domaine.Entities;

namespace Infrastructure
{
	public class ApplicationContext : DbContext
	{
		public DbSet<CivilState> CivilStates { get; set; }
		public DbSet<Profile> Profiles { get; set; }
	}
}