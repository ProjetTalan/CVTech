using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface ICivilStateService
	{
		Task<CivilStateModel> GetCivilStateByIdAsync(int? id);
		Task<IEnumerable<CivilStateModel>> GetAllCivilStatesAsync();
		Task<int> CreateAsync(CivilStateModel civilStateToAdd);
		Task<int> UpdateAsync(int id, CivilStateModel civilStateToUpdate);
		Task<bool> RemoveCivilStateAsync(int civilStateId);
	}
}