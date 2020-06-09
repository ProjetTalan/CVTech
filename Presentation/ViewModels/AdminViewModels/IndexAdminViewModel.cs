using System.Collections.Generic;
using Application.Models;

namespace Presentation.ViewModels.AdminViewModels
{
	public class IndexAdminViewModel
	{
		public IList<ProfileModel> ProfileModels { get; set; }
	}
}