using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.AdminViewModels
{
	public class CreateProfileViewModel
	{
		//TODO que ça soit dans la view et ici

		[Required(ErrorMessage = "Please enter a valid first name")]
		[MaxLength(30), MinLength(3)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter a valid last name")]
		[MaxLength(30), MinLength(3)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter a valid address")]
		[MaxLength(30), MinLength(10)]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter a valid address")]
		[MaxLength(30), MinLength(10)]
		[PasswordPropertyText]
		public string Password { get; set; }

		public int RoleNumber { get; set; }

		[Required(ErrorMessage = "Please enter a valid date")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

	}
}