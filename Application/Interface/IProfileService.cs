using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface IProfileService
	{
		Task<ProfileModel> GetProfileByIdAsync(int? id);
		Task<IEnumerable<ProfileModel>> GetAllProfilesAsync();
		Task<int> CreateAsync(ProfileModel profilModelToAdd);
		Task<int> UpdateAsync(int id, ProfileModel profilModelToUpdate);
		Task<bool> RemoveProfileAsync(int profilModelId);
		Task<bool> UserExist(ProfileModel profileToCheck);
		Task<ProfileModel> GetProfileByModelAsync(ProfileModel profilModel);
	}
}