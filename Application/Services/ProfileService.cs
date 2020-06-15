using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Application.Interface;
using Application.Models;
using Domaine.Entities;
using Infrastructure;

namespace Application.Services
{
	public class ProfileService : IProfileService
	{
		public ProfileService()
		{
			var unused = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}
		public async Task<ProfileModel> GetProfileByIdAsync(int? id)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.Profiles
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
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<ProfileModel> GetProfileByModelAsync(ProfileModel profilModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.Profiles
					.Where(x => x.Email == profilModel.Email && x.Password == profilModel.Password)
					.Select(x => new ProfileModel
					{
						Id = x.Id,
						FirstName = x.FirstName,
						LastName = x.LastName,
						Email = x.Email,
						Password = x.Password,
						Role = x.Role
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<ProfileModel>> GetAllProfilesAsync()
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.Profiles.Select(x => new ProfileModel
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					Password = x.Password,
					Role = x.Role
				}).ToListAsync();
			}
		}

		public async Task<int> CreateAsync(ProfileModel profileToAdd)
		{
			using (ApplicationContext context = new ApplicationContext())
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

				context.Profiles.Add(toAdd);
				await context.SaveChangesAsync();

				return toAdd.Id;

			}
		}

		public async Task<int> UpdateAsync(int id, ProfileModel profileToUpdate)
		{
			using (ApplicationContext context = new ApplicationContext())
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
				context.Profiles.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity.Id;
			}
		}

		public async Task<bool> RemoveProfileAsync(int profileId)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				Profile entity = await context.Profiles.FirstOrDefaultAsync(x => x.Id == profileId);
				if (entity != null)
				{
					context.Profiles.Remove(entity);
				}

				await context.SaveChangesAsync();
				return true;
			}
		}

		public async Task<bool> UserExist(ProfileModel profileToCheck)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.Profiles.AnyAsync(x =>
					x.Email == profileToCheck.Email && x.Password == profileToCheck.Password);
			}
		}
	}
}