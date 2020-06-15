using System;
using System.Data.Entity;
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
	}
}