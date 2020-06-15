using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.AdminViewModels
{
	public class EditProfileViewModel
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
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter a valid address")]
		[MaxLength(30), MinLength(10)]
		public string Password { get; set; }

		public int RoleNumber { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime DateOfBirth { get; set; }
	}
}