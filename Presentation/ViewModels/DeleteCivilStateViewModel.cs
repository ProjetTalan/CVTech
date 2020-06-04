using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
	public class DeleteCivilStateViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM / dd / yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfBirth { get; set; }
	}
}