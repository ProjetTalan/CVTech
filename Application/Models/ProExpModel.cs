using System;
using System.Collections.Generic;

namespace Application.Models
{
	public class ProExpModel
	{
		public int Id { get; set; }
		public int ProfileId { get; set; }
		public int CompaniesId { get; set; }
		public string CompanyName { get; set; }
		public int CityId { get; set; }
		public string CityName { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		public IList<TechnologyModel> TechnologyModels { get; set; }

	}
}