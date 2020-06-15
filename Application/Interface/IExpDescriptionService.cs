using System.Threading.Tasks;
using Application.Models;

namespace Application.Interface
{
	public interface IExpDescriptionService
	{
		Task<int> CreateAsync(ExperienceDescriptionModel expDescModel);
	}
}