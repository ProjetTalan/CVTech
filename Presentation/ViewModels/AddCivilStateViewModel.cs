using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
	public class AddCivilStateViewModel
	{
		[Required(ErrorMessage = "Please enter a valid first name")]
		[MaxLength(30), MinLength(3)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter a valid last name")]
		[MaxLength(30), MinLength(3)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter a valid address")]
		[MaxLength(30), MinLength(10)]
		public string Address { get; set; }

		[Required(ErrorMessage = "Please enter a valid date")]
		[DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }
	}
}