using System;
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
	public class ExpDescriptionService : IExpDescriptionService
	{
		public async Task<int> CreateAsync(ExperienceDescriptionModel expDescModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				var entity = new ExperienceDescription
				{
					Description = expDescModel.Description,
					Name = expDescModel.Name,
					ProExpId = expDescModel.ProExpId,
				};

				var position =
					await context.Positions
						.FirstOrDefaultAsync(x => x.Description == expDescModel.PositionDesc);

				if (position == null)
				{
					position = new Position
					{
						Description = expDescModel.PositionDesc
					};
				}

				entity.Position = position;

				context.ExperienceDescriptions.Add(entity);
				await context.SaveChangesAsync();

				return entity.Id;
			}
		}

		// TODO réaliser complètement ce service pour alléger les fonctions du service ProExpService ??
		// Oui car on peut l'ajouter après une fermeture d'using, est ce bien? Askip ça évitera d'avoir une fonction de 150 lignes

		public async Task<int> UpdateAsync(int id, ExperienceDescriptionModel expDescModel)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				var entity = new ExperienceDescription
				{
					Id = id,
					Description = expDescModel.Description,
					Name = expDescModel.Name,
					ProExpId = expDescModel.ProExpId,
				};

				var position =
					await context.Positions
						.FirstOrDefaultAsync(x => x.Description == expDescModel.PositionDesc);

				if (position == null)
				{
					position = new Position
					{
						Description = expDescModel.PositionDesc
					};
				}

				entity.Position = position;

				context.ExperienceDescriptions.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity.Id;
			}
		}

		public async Task<bool> RemoveAsync(int id)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				ExperienceDescription entity = await context.ExperienceDescriptions.FirstOrDefaultAsync(x => x.Id == id);
				if (entity != null)
				{
					context.ExperienceDescriptions.Remove(entity);
				}

				await context.SaveChangesAsync();
				return true;
			}
		}

		public async Task<ExperienceDescriptionModel> GetExpDescriptionByProExpId(int proExpId)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				return await context.ExperienceDescriptions
					.Where(x => x.ProExpId == proExpId)
					.Select(x => new ExperienceDescriptionModel
					{
						Name = x.Name,
						Id = x.Id,
						Description = x.Description,
						ProExpId = x.ProExpId,
						PositionDesc = x.Position.Description
					}).SingleOrDefaultAsync();
			}
		}
	}
}