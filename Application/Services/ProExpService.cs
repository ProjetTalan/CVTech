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
	public class ProExpService : IProExpService
	{
		private ExpDescriptionService _expDescriptionService;
		public ProExpService()
		{
			_expDescriptionService = new ExpDescriptionService();
		}
		// Conservation d'un seul service qui s'occupe de l'expérience professionnel et qui ajoute des données dans différentes tables.
		// TODO voir pour refacto ces deux fonctions assez similaires
		public async Task<int> CreateAsync(ProExpModel proExpModel)
		{
			ProExp entity = new ProExp
			{
				ProfileId = proExpModel.ProfileId,
				FromDate = proExpModel.FromDate,
				ToDate = proExpModel.ToDate
			};

			using (ApplicationContext context = new ApplicationContext())
			{
				
				// CITY PART
				var city = await context.Cities
					.FirstOrDefaultAsync(x => x.Name == proExpModel.CityName);

				if (city == null)
				{
					city = new City
					{
						Name = proExpModel.CityName
					};
				}

				entity.City = city;

				// COMPANY PART
				var company = await context.Companies
					.FirstOrDefaultAsync(x => x.Name == proExpModel.CompanyName);

				if (company == null)
				{
					company = new Company
					{
						Name = proExpModel.CompanyName
					};
				}

				entity.Company = company;

				// TECHNOLOGY PART
				if (proExpModel.TechnologyModels != null &&
					proExpModel.TechnologyModels.Count > 0)
				{
					foreach (var technologyModel in proExpModel.TechnologyModels)
					{
						Technology technology = await context.Technologies.FirstOrDefaultAsync(x => x.Title == technologyModel.Title);

						if (technology == null)
						{
							technology = new Technology
							{
								Title = technologyModel.Title
							};
						}

						entity.Technologies.Add(technology);

						context.ProfileTechnologies.AddOrUpdate(new ProfileTechnology
						{
							ProfileId = proExpModel.ProfileId,
							TechLevelId = technologyModel.TechLevelId,
							Technology = technology
						});
					}
				}

				context.ProExps.Add(entity);
				await context.SaveChangesAsync();
			}

			// Description Part with other Service

			var description = proExpModel.ExperienceDescriptionModel;
			if (description != null)
			{
				description.ProExpId = entity.Id;
				await _expDescriptionService.CreateAsync(description);
			}

			return entity.Id;
		}

		public async Task<int> UpdateAsync(int id, ProExpModel proExpModel)
		{
			ProExp entity = new ProExp
			{
				Id = id,
				ProfileId = proExpModel.ProfileId,
				FromDate = proExpModel.FromDate,
				ToDate = proExpModel.ToDate
			};

			using (ApplicationContext context = new ApplicationContext())
			{
				var city = await context.Cities
					.FirstOrDefaultAsync(x => x.Name == proExpModel.CityName);

				if (city == null)
				{
					city = new City
					{
						Name = proExpModel.CityName
					};
				}

				entity.City = city;

				// COMPANY PART
				var company = await context.Companies
					.FirstOrDefaultAsync(x => x.Name == proExpModel.CompanyName);

				if (company == null)
				{
					company = new Company
					{
						Name = proExpModel.CompanyName
					};
				}

				entity.Company = company;

				if (proExpModel.TechnologyModels != null &&
				    proExpModel.TechnologyModels.Count > 0)
				{
					foreach (var technologyModel in proExpModel.TechnologyModels)
					{
						Technology technology = await context.Technologies.FirstOrDefaultAsync(x => x.Title == technologyModel.Title);

						if (technology == null)
						{
							technology = new Technology
							{
								Title = technologyModel.Title
							};
						}

						entity.Technologies.Add(technology);

						context.ProfileTechnologies.AddOrUpdate(new ProfileTechnology
						{
							ProfileId = proExpModel.ProfileId,
							TechLevelId = technologyModel.TechLevelId,
							Technology = technology
						});
					}
				}
				else
				{
					entity.Technologies = null;
				}

				context.ProExps.AddOrUpdate(entity);
				await context.SaveChangesAsync();
			}

			// Description Part with other Service

			var descriptionUpdated = proExpModel.ExperienceDescriptionModel;
			if (descriptionUpdated != null)
			{
				var descriptionInDb = await _expDescriptionService.GetExpDescriptionByProExpId(entity.Id);
				if (descriptionInDb == null)
				{
					await _expDescriptionService.CreateAsync(descriptionUpdated);
				}
				else
				{
					await _expDescriptionService.UpdateAsync(descriptionInDb.Id, descriptionUpdated);
				}
			}

			return entity.Id;
		}

		public async Task<IEnumerable<ProExpModel>> GetAllProExpFrom(ProfileModel profileModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.ProExps
					.Where(x => x.ProfileId == profileModel.Id)
					.Select(x => new ProExpModel
					{
						Id = x.Id,
						CityName = x.City.Name,
						CompanyName = x.Company.Name,
						FromDate = x.FromDate,
						ToDate = x.ToDate
					}).ToListAsync();
			}
		}

		public async Task<ProExpModel> GetProExpByIdAsync(int id)
		{
			ProExpModel toReturn;

			using (ApplicationContext context = new ApplicationContext())
			{
				toReturn = await context.ProExps.Where(x => x.Id == id).Select(x => new ProExpModel
				{
					Id = id,
					CompanyName = x.Company.Name,
					CityName = x.City.Name,
					FromDate = x.FromDate,
					ToDate = x.ToDate
				}).SingleOrDefaultAsync();
			}

			if (toReturn != null)
			{
				toReturn.ExperienceDescriptionModel = await _expDescriptionService.GetExpDescriptionByProExpId(id);
			}
			
			return toReturn;
		}
		public async Task<IEnumerable<ProfileTechModel>> GetAllTechnoFrom(ProExpModel proExpModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.ProExps
					.Where(x => x.Id == proExpModel.Id)
					.SelectMany(x => x.Technologies).Join(
						context.ProfileTechnologies,
						techFromProExp => techFromProExp.Id,
						profileTech => profileTech.TechnologyId,
						(proExpTech, profileTech) => new ProfileTechModel()
						{
							TechnologyName = proExpTech.Title,
							TechLevelId = profileTech.TechLevelId,
							TechLevelDescription = profileTech.TechLevel.Description,
							ProfileId = profileTech.ProfileId,
							TechnologyId = profileTech.TechnologyId
						}).ToListAsync();
			}
		}

		public async Task<bool> RemoveAsync(int id)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				ProExp entity = await context.ProExps.FirstOrDefaultAsync(x => x.Id == id);
				if (entity != null)
				{
					context.ProExps.Remove(entity);
				}

				await context.SaveChangesAsync();
				return true;
			}
		}
	}
}