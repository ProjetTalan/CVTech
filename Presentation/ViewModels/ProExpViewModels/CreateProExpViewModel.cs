using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.ProExpViewModels
{
	public class CreateProExpViewModel
	{
		[Required(ErrorMessage = "Please enter a valid date")]
		[DataType(DataType.Date)]
		public DateTime FromDate { get; set; }

		[Required(ErrorMessage = "Please enter a valid date")]
		[DataType(DataType.Date)]
		public DateTime ToDate { get; set; }

		public string CompanyName { get; set; }
		public string CityName { get; set; }

		[DataType(DataType.MultilineText)]
		public string ProExpDescription { get; set; }
		public string ProExpPosition { get; set; }

		public string ProExpName { get; set; }
	}
}