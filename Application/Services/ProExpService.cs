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
	public class ProExpService : IProExpService
    {
        private readonly IApplicationContext _dbContext;

		public ProExpService(IApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<int> CreateAsync(ProExpModel proExpModel)
		{
			ProExp entity = new ProExp
			{
				ProfileId = proExpModel.ProfileId,
				FromDate = proExpModel.FromDate,
				ToDate = proExpModel.ToDate,
				City = await GetOrCreateCityAsync(proExpModel.CityName),
				Company = await GetOrCreateCompanyAsync(proExpModel.CompanyName),
				Technologies = await GetOrCreateTechnoloyAsync(proExpModel.TechnologyModels, proExpModel.ProfileId),
				ExperienceDescriptions = await CreateOrUpdateExpDescAsync(proExpModel.ExperienceDescriptionModel)
			};

			_dbContext.ProExps.Add(entity);
			await _dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<int> UpdateAsync(int id, ProExpModel proExpModel)
		{
			ProExp entity = new ProExp
			{
				Id = id,
				ProfileId = proExpModel.ProfileId,
				FromDate = proExpModel.FromDate,
				ToDate = proExpModel.ToDate,
				City = await GetOrCreateCityAsync(proExpModel.CityName),
				Company = await GetOrCreateCompanyAsync(proExpModel.CompanyName),
				Technologies = await GetOrCreateTechnoloyAsync(proExpModel.TechnologyModels, proExpModel.ProfileId),
				ExperienceDescriptions = await CreateOrUpdateExpDescAsync(proExpModel.ExperienceDescriptionModel)
			};

			_dbContext.ProExps.AddOrUpdate(entity);
			await _dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<IEnumerable<ProExpModel>> GetAllProExpFrom(ProfileModel profileModel)
		{
			return await _dbContext.ProExps
				.Where(x => x.ProfileId == profileModel.Id)
				.Select(x => new ProExpModel
				{
					Id = x.Id,
					CityName = x.City.Name,
					CompanyName = x.Company.Name,
					FromDate = x.FromDate,
					ToDate = x.ToDate
				})
				.ToListAsync();
		}

		public async Task<ProExpModel> GetProExpByIdAsync(int id)
		{
			ProExpModel toReturn = 
				await _dbContext.ProExps
				.Where(x => x.Id == id)
				.Select(x => new ProExpModel
				{
					Id = id,
					CompanyName = x.Company.Name,
					CityName = x.City.Name,
					FromDate = x.FromDate,
					ToDate = x.ToDate
				})
				.SingleOrDefaultAsync();

			if (toReturn != null)
			{
				toReturn.ExperienceDescriptionModel = await GetExpDescriptionByProExpId(id);
			}
			
			return toReturn;
		}
		public async Task<IEnumerable<ProfileTechModel>> GetAllTechnoFrom(ProExpModel proExpModel)
		{
			
				return await _dbContext.ProExps
					.Where(x => x.Id == proExpModel.Id)
					.SelectMany(x => x.Technologies).Join(
						_dbContext.ProfileTechnologies,
						techFromProExp => techFromProExp.Id,
						profileTech => profileTech.TechnologyId,
						(proExpTech, profileTech) => new ProfileTechModel()
						{
							TechnologyName = proExpTech.Title,
							TechLevelId = profileTech.TechLevelId,
							TechLevelDescription = profileTech.TechLevel.Description,
							ProfileId = profileTech.ProfileId,
							TechnologyId = profileTech.TechnologyId
						})
					.ToListAsync();
		}

		public async Task<bool> RemoveAsync(int id)
		{
			ProExp entity = await _dbContext.ProExps.FirstOrDefaultAsync(x => x.Id == id);
			if (entity != null)
			{
				var profileTechnologies = 
					await _dbContext.ProExps
					.Where(x => x.Id == id)
					.SelectMany(x => x.Technologies)
					.Join(_dbContext.ProfileTechnologies,
						techFromProExp => techFromProExp.Id,
						profileTech => profileTech.TechnologyId,
						(proExpTech, profileTech) => new ProfileTechnology()
						{
							ProfileId = profileTech.ProfileId,
							TechnologyId = profileTech.TechnologyId
						})
						.ToListAsync();

				foreach (var profileTechnology in profileTechnologies)
				{
					_dbContext.ProfileTechnologies.Remove(profileTechnology);
				}
				_dbContext.ProExps.Remove(entity);
			}

			await _dbContext.SaveChangesAsync();
			return true;
		}

		// PRIVATE PORTION, entity that will not be created outside of here
		private async Task<City> GetOrCreateCityAsync(string cityName)
		{
			var city = await _dbContext.Cities
				.FirstOrDefaultAsync(x => x.Name == cityName);

			if (city == null)
			{
				city = new City
				{
					Name = cityName
				};
			}

			return city;
		}

		private async Task<Company> GetOrCreateCompanyAsync(string companyName)
		{
			var company = await _dbContext.Companies
				.FirstOrDefaultAsync(x => x.Name == companyName);

			if (company == null)
			{
				company = new Company
				{
					Name = companyName
				};
			}
			return company;
		}

		private async Task<List<Technology>> GetOrCreateTechnoloyAsync(IList<TechnologyModel> technologyModels, int profileId)
		{
			if (technologyModels == null || technologyModels.Count <= 0)
			{
				return null;
			}

			List<Technology> toReturn = new List<Technology>();
			foreach (var technologyModel in technologyModels)
			{
				Technology technology = await _dbContext.Technologies.FirstOrDefaultAsync(x => x.Title == technologyModel.Title);

				if (technology == null)
				{
					technology = new Technology
					{
						Title = technologyModel.Title
					};
				}

				toReturn.Add(technology);

				_dbContext.ProfileTechnologies.AddOrUpdate(new ProfileTechnology
				{
					ProfileId = profileId,
					TechLevelId = technologyModel.TechLevelId,
					Technology = technology
				});
			}
			return toReturn;
		}

		private async Task<Position> GetOrCreatePositionAsync(string positionDesc)
		{
			var position =
				await _dbContext.Positions
					.FirstOrDefaultAsync(x => x.Description == positionDesc);

			if (position == null)
			{
				position = new Position
				{
					Description = positionDesc
				};
			}

			return position;
		}

		private async Task<List<ExperienceDescription>> CreateOrUpdateExpDescAsync 
			(IList<ExperienceDescriptionModel> experienceDescriptionModels)
		{
			if (experienceDescriptionModels == null || experienceDescriptionModels.Count <= 0)
			{
				return null;
			}

			List<ExperienceDescription> toReturn = new List<ExperienceDescription>();
			foreach (var expDescription in experienceDescriptionModels)
			{
				var expDescTemp = new ExperienceDescription();

				if (expDescription.Id != null)
				{
					expDescTemp.Id = expDescription.Id.Value;
					_dbContext.ExperienceDescriptions.Attach(expDescTemp);
				}

				expDescTemp.Name = expDescription.Name;
				expDescTemp.Description = expDescription.Description;
				expDescTemp.Position = await GetOrCreatePositionAsync(expDescription.PositionDesc);

				toReturn.Add(expDescTemp);
			}
			return toReturn;
		}

		private async Task<List<ExperienceDescriptionModel>> GetExpDescriptionByProExpId(int proExpId)
		{
			return await _dbContext.ExperienceDescriptions
				.Where(x => x.ProExpId == proExpId)
				.Select(x => new ExperienceDescriptionModel
				{
					Name = x.Name,
					Id = x.Id,
					Description = x.Description,
					ProExpId = x.ProExpId,
					PositionDesc = x.Position.Description
				})
				.ToListAsync();
		}
	}
}