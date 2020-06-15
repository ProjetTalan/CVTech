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
	public class ProExpService : IProExpService
	{
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
				return entity.Id;
			}
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
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.ProExps.Where(x => x.Id == id).Select(x => new ProExpModel
				{
					Id = id,
					CompanyName = x.Company.Name,
					CityName = x.City.Name,
					FromDate = x.FromDate,
					ToDate = x.ToDate
				}).SingleOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<ProfileTechModel>> GetAllTechnoFrom(ProExpModel proExpModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.ProExps
					.Where(x => x.Id == proExpModel.Id)
					.SelectMany(x => x.Technologies).Join(
						context.ProfileTechnologies,
						proExpTech => proExpTech.Id,
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

	}
}