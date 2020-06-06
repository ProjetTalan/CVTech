using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface IProfileService
	{
		Task<ProfileModel> GetProfileByIdAsync(int? id);
		Task<IEnumerable<ProfileModel>> GetAllProfilesAsync();
		Task<int> CreateAsync(ProfileModel civilStateToAdd);
		Task<int> UpdateAsync(int id, ProfileModel civilStateToUpdate);
		Task<bool> RemoveProfileAsync(int civilStateId);
		Task<bool> UserExist(ProfileModel profileToCheck);
	}
}