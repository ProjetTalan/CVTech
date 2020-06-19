using Application.Interface;
using Application.Models;

using Domaine.Entities;

using Infrastructure;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    class LanguageService : ILanguageService
    {
        public async Task<int> CreateAsync(LanguageFluenciesModel fluenciesModel)
        {
            LanguageFluencies fluencies = new LanguageFluencies()
            {
                ProfileID = fluenciesModel.ProfileID,

                ConversationFluencyID = fluenciesModel.ConversationFluencyID,
                SpokenFluencyID = fluenciesModel.SpokenFluencyID,
                TechnicalVocabFluencyID = fluenciesModel.TechnicalVocabFluencyID,
                WrittenComprehensionFluencyID = fluenciesModel.WrittenComprehensionFluencyID,
                WrittenExpressionFluencyID = fluenciesModel.WrittenExpressionFluencyID
            };

			using (ApplicationContext db = new ApplicationContext())
			{

				// Lang PART
				var lang = await db.Languages
					.FirstOrDefaultAsync(x => x.Language == fluenciesModel.LanguagesName);

				if (lang == null)
				{
					lang = new Languages
					{
						Language = fluenciesModel.LanguagesName
					};
				}

				fluencies.Languages = lang;

				db.LanguageFluencies.Add(fluencies);
				await db.SaveChangesAsync();

                return fluencies.LanguageID;
			}
		}

        public async Task<IEnumerable<ProfileTechModel>> GetAllProfileTechFrom(LanguageFluenciesModel fluenciesModel)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.ProExps
                    .Where(x => x.Id == fluenciesModel.ProfileID)
                    .SelectMany(x => x.Technologies).Join(
                        db.ProfileTechnologies,
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

        public async Task<IEnumerable<LanguageFluenciesModel>> GetAllLanguFrom(ProfileModel profileModel)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.LanguageFluencies
                    .Where(x => x.ProfileID == profileModel.Id)
            }
        }

        public Task<ProExpModel> GetLanguFluencyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, LanguageFluenciesModel fluenciesModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProfileModel>> GetAllProfilesFrom(LanguageFluenciesModel fluenciesModel)
        {
            throw new NotImplementedException();
        }
    }
}
