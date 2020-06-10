using System;
using System.ComponentModel.DataAnnotations;

using Domaine.Entities;

namespace Application.Models
{
	public class ProfileModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public string ImageUrl { get; set; }
		public string RoleName
		{
			get { return Role.ToString(); }
		}
		
		[Required]
		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfBirth { get; set; }
	}
}