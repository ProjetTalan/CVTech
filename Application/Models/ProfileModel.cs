using System;
using System.ComponentModel.DataAnnotations;
using Domaine.Entities;

namespace Application.Models
{
	public class ProfileModel
	{
		public int Id { get; set; }

        [Display(Name = "First Name")]
		public string FirstName { get; set; }
        [Display(Name = "Last Name")]
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
        [Display(Name = "Date of birth")]
		public DateTime DateOfBirth { get; set; }
		public Role Role { get; set; }
		public string ImageUrl { get; set; }
		public Gender Gender { get; set; }

        [Display(Name = "Role")]
		public string RoleName
		{
			get { return Role.ToString(); }
		}
	}
}