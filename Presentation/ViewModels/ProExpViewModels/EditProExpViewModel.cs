using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.ProExpViewModels
{
	public class EditProExpViewModel
	{
		public int Id { get; set; }
		[DataType(DataType.Date)]
		public DateTime FromDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime ToDate { get; set; }

		public string CompanyName { get; set; }
		public string CityName { get; set; }
	}
}