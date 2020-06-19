using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface IExpDescriptionService
	{
		Task<int> CreateAsync(ExperienceDescriptionModel expDescModel);
		Task<int> UpdateAsync(int id, ExperienceDescriptionModel expDescModel);
		Task<ExperienceDescriptionModel> GetExpDescriptionByProExpId(int proExpId);
		Task<bool> RemoveAsync(int id);
	}
}