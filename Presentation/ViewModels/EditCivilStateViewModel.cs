using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
	public class EditCivilStateViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please enter a valid first name")]
		[MaxLength(30), MinLength(3)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter a valid last name")]
		[MaxLength(30), MinLength(3)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter a valid address")]
		[MaxLength(30), MinLength(10)]
		public string Address { get; set; }

		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }
	}
}