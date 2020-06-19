using System;
using System.Collections.Generic;
using Application.Models;

namespace Presentation.ViewModels.ProExpViewModels
{
	public class DetailsProExpViewModel
	{
		public int Id { get; set; }
		public int ProfileId { get; set; }
		public int CompaniesId { get; set; }
		public string CompanyName { get; set; }
		public int CityId { get; set; }
		public string CityName { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		public ExperienceDescriptionModel ExperienceDescriptionModel { get; set; }
		public IList<ExperienceDescriptionModel> ExperienceDescriptionModels { get; set; }
		public IList<ProfileTechModel> ProfileTechModels { get; set; }
	}
}