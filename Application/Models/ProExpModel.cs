using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
	public class ProExpModel
	{
		public int Id { get; set; }
		public int ProfileId { get; set; }
		public int CompaniesId { get; set; }
        [Display(Name = "Company name")]
		public string CompanyName { get; set; }
		public int CityId { get; set; }
        [Display(Name = "City name")]
		public string CityName { get; set; }
        [Display(Name = "From date")]
		public DateTime FromDate { get; set; }
        [Display(Name = "To date")]
		public DateTime ToDate { get; set; }

		public ExperienceDescriptionModel ExperienceDescriptionModel { get; set; }
		public IList<TechnologyModel> TechnologyModels { get; set; }

	}
}