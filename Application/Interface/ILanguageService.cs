using Application.Models;

using Domaine.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    interface ILanguageService
    {
        Task<int> CreateAsync(LanguageFluenciesModel fluenciesModel);

		Task<IEnumerable<LanguageFluenciesModel>> GetAllLanguFrom(ProfileModel profileModel);

		Task<ProExpModel> GetLanguFluencyByIdAsync(int id);

		Task<IEnumerable<ProfileModel>> GetAllProfilesFrom(LanguageFluenciesModel fluenciesModel);

		Task<int> UpdateAsync(int id, LanguageFluenciesModel fluenciesModel);
		Task<bool> RemoveAsync(int id);
    }
}
