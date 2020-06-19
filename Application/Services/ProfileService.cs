using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Application.Models;
using Domaine.Entities;

namespace Application.Services
{
	public class ProfileService : IProfileService
	{
		private IApplicationContext _dbContext;
		public ProfileService(IApplicationContext context)
		{
			var unused = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
			_dbContext = context;
		}
		public async Task<ProfileModel> GetProfileByIdAsync(int? id)
		{
			return await _dbContext.Profiles
				.Where(x => x.Id == id)
				.Select(x => new ProfileModel
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					Password = x.Password,
					Role = x.Role,
					DateOfBirth = x.DateOfBirth
				})
				.FirstOrDefaultAsync();
		}

		public async Task<ProfileModel> GetProfileByModelAsync(ProfileModel profilModel)
		{
			return await _dbContext.Profiles
				.Where(x => x.Email == profilModel.Email && x.Password == profilModel.Password)
				.Select(x => new ProfileModel
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					Password = x.Password,
					Role = x.Role
				})
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<ProfileModel>> GetAllProfilesAsync()
		{
			return await _dbContext.Profiles
				.Select(x => new ProfileModel
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					Password = x.Password,
					Role = x.Role
				})
				.ToListAsync();
		}

		public async Task<int> CreateAsync(ProfileModel profileToAdd)
		{
			Profile toAdd = new Profile
			{
				FirstName = profileToAdd.FirstName,
				LastName = profileToAdd.LastName,
				Email = profileToAdd.Email,
				Password = profileToAdd.Password,
				Role = profileToAdd.Role,
				DateOfBirth = profileToAdd.DateOfBirth
			};

			_dbContext.Profiles.Add(toAdd);
			await _dbContext.SaveChangesAsync();

			return toAdd.Id;
		}

		public async Task<int> UpdateAsync(int id, ProfileModel profileToUpdate)
		{
			Profile entity = new Profile
			{
				Id = id,
				FirstName = profileToUpdate.FirstName,
				LastName = profileToUpdate.LastName,
				Email = profileToUpdate.Email,
				Password = profileToUpdate.Password,
				Role = profileToUpdate.Role,
				DateOfBirth = profileToUpdate.DateOfBirth
			};
			_dbContext.Profiles.AddOrUpdate(entity);
			await _dbContext.SaveChangesAsync();

			return entity.Id;
		}

		public async Task<bool> RemoveAsync(int profileId)
		{
			Profile entity = await _dbContext.Profiles.FirstOrDefaultAsync(x => x.Id == profileId);
			if (entity != null)
			{
				_dbContext.Profiles.Remove(entity);
			}

			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UserExist(ProfileModel profileToCheck)
		{
			return await _dbContext.Profiles.AnyAsync(x =>
				x.Email == profileToCheck.Email && x.Password == profileToCheck.Password);
			
		}
	}
}