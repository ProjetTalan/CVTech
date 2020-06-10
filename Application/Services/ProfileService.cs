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
					.Where(x => x.ProfileID == id)
					.Select(x => new ProfileModel
					{
						Id = x.ProfileID,
						FirstName = x.FirstName,
						LastName = x.LastName,
						Email = x.Email,
						Password = x.ProfilePassword,
						Role = x.ProfileMainType
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<ProfileModel> GetProfileByModelAsync(ProfileModel profilModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.Profiles
					.Where(x => x.Email == profilModel.Email && x.ProfilePassword == profilModel.Password)
					.Select(x => new ProfileModel
					{
						Id = x.ProfileID,
						FirstName = x.FirstName,
						LastName = x.LastName,
						Email = x.Email,
						Password = x.ProfilePassword,
						Role = x.ProfileMainType
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<ProfileModel>> GetAllProfilesAsync()
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.Profiles.Select(x => new ProfileModel
				{
					Id = x.ProfileID,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					Password = x.ProfilePassword,
					Role = x.ProfileMainType
				}).ToListAsync();
			}
		}

		public async Task<int> CreateAsync(ProfileModel profileToAdd)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				Profiles toAdd = new Profiles
				{
					FirstName = profileToAdd.FirstName,
					LastName = profileToAdd.LastName,
					Email = profileToAdd.Email,
					ProfilePassword = profileToAdd.Password,
					ProfileMainType = profileToAdd.Role,
					//TODO changement de date pour une vrai valeur
					DateOfBirth = DateTime.Today
				};

				context.Profiles.Add(toAdd);
				await context.SaveChangesAsync();

				return toAdd.ProfileID;

			}
		}

		public async Task<int> UpdateAsync(int id, ProfileModel profileToUpdate)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				Profiles entity = new Profiles
				{
					ProfileID = id,
					FirstName = profileToUpdate.FirstName,
					LastName = profileToUpdate.LastName,
					Email = profileToUpdate.Email,
					ProfilePassword = profileToUpdate.Password,
					ProfileMainType = profileToUpdate.Role
				};
				context.Profiles.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity.ProfileID;
			}
		}

		public async Task<bool> RemoveProfileAsync(int profileId)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				Profiles entity = await context.Profiles.FirstOrDefaultAsync(x => x.ProfileID == profileId);
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
					x.Email == profileToCheck.Email && x.ProfilePassword == profileToCheck.Password);
			}
		}
	}
}