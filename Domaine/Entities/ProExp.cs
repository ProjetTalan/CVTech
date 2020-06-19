using System;
using System.Collections.Generic;

namespace Domaine.Entities
{
	public class ProExp
	{
		public int Id { get; set; }
		public int ProfileId { get; set; }
		public int CompaniesId { get; set; }
		public Company Company { get; set; }
		public int CityId { get; set; }
		public City City { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		public ICollection<Technology> Technologies { get; set; }
	}
}