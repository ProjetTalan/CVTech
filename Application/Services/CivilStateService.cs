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
	public class CivilStateService : ICivilStateService
	{
		public async Task<CivilStateModel> GetCivilStateByIdAsync(int? id)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.CivilStates
					.Where(x => x.Id == id)
					.Select(x => new CivilStateModel
					{
						Id = x.Id,
						FirstName = x.FirstName,
						LastName = x.LastName,
						Address = x.Address,
						DateOfBirth = x.DateOfBirth
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<CivilStateModel>> GetAllCivilStatesAsync()
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.CivilStates.Select(x => new CivilStateModel
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Address = x.Address,
					DateOfBirth = x.DateOfBirth
				}).ToListAsync();
			}
		}

		public async Task<int> CreateAsync(CivilStateModel civilStateToAdd)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				if (civilStateToAdd.DateOfBirth == null) return civilStateToAdd.Id;

				CivilState toAdd = new CivilState
				{
					FirstName = civilStateToAdd.FirstName,
					LastName = civilStateToAdd.LastName,
					Address = civilStateToAdd.Address,
					DateOfBirth = civilStateToAdd.DateOfBirth.Value
				};

				context.CivilStates.Add(toAdd);
				await context.SaveChangesAsync();

				return toAdd.Id;

			}
		}

		public async Task<int> UpdateAsync(int id, CivilStateModel civilStateToUpdate)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				if (civilStateToUpdate.DateOfBirth == null) return civilStateToUpdate.Id;

				CivilState entity = new CivilState
				{
					Id = id,
					FirstName = civilStateToUpdate.FirstName,
					LastName = civilStateToUpdate.LastName,
					Address = civilStateToUpdate.Address,
					DateOfBirth = civilStateToUpdate.DateOfBirth.Value
				};
				context.CivilStates.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity.Id;
			}
		}

		public async Task<bool> RemoveCivilStateAsync(int civilStateId)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				CivilState entity = await context.CivilStates.FirstOrDefaultAsync(x => x.Id == civilStateId);
				if (entity != null)
				{
					context.CivilStates.Remove(entity);
				}

				await context.SaveChangesAsync();
				return true;
			}
		}
	}
}