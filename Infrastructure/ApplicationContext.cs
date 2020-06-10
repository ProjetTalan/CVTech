using System.Data.Entity;
using Domaine.Entities;

namespace Infrastructure
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext() : base("name=Talan") {}
		public DbSet<Profiles> Profiles { get; set; }
	}
}