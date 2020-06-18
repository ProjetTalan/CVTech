using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface IProExpService
	{
		Task<int> CreateAsync(ProExpModel proExpModel);

		Task<IEnumerable<ProExpModel>> GetAllProExpFrom(ProfileModel profileModel);

		Task<ProExpModel> GetProExpByIdAsync(int id);

		Task<IEnumerable<ProfileTechModel>> GetAllTechnoFrom(ProExpModel proExpModel);

		Task<int> UpdateAsync(int id, ProExpModel proExpToUpdate);
		Task<bool> RemoveAsync(int id);
	}
}