using System.Data.Entity;
using Application.Interface;
using Domaine.Entities;

namespace Infrastructure
{
	public class ApplicationContext : DbContext, IApplicationContext
	{
		public ApplicationContext() : base("name=TalanCvTheque") {}
		public DbSet<Profile> Profiles { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Technology> Technologies { get; set; }
		public DbSet<ProExp> ProExps { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<ProfileTechnology> ProfileTechnologies { get; set; }
		public DbSet<TechLevel> TechLevels { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<ExperienceDescription> ExperienceDescriptions { get; set; }
		public DbSet<Fluency> Fluencies { get; set; }
		public DbSet<LanguageFluencies> LanguageFluencies { get; set; }
		public DbSet<Languages> Languages { get; set; }
	}
}