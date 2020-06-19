using Domaine.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationContext
    {
	    DbSet<Profile> Profiles { get; }
	    DbSet<Company> Companies { get; }
        DbSet<City> Cities { get; }
        DbSet<Technology> Technologies { get; }
		DbSet<ProExp> ProExps { get; }
        DbSet<Country> Countries { get; }
        DbSet<ProfileTechnology> ProfileTechnologies { get; }
        DbSet<TechLevel> TechLevels { get; }
        DbSet<Position> Positions { get; }
        DbSet<ExperienceDescription> ExperienceDescriptions { get; }
        DbSet<Fluency> Fluencies { get; }
        DbSet<LanguageFluencies> LanguageFluencies { get; }
        DbSet<Languages> Languages { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}