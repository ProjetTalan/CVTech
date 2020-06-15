using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface IProExpService
	{
		Task<int> CreateAsync(ProExpModel proExpModel);

		Task<IEnumerable<ProExpModel>> GetAllProExpFrom(ProfileModel profileModel);
	}
}