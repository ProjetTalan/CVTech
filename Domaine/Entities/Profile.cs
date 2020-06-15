using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domaine.Entities
{
	public enum Gender
	{
		Male = 0,
		Female = 1
	}
	public enum Role
	{
		Consultant = 0,
		Recruiter = 1,
		Admin = 2
	}
	public class Profile
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Gender Gender { get; set; }
		public string Nationality { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }
		public string Zip { get; set; }
		public int? CityId { get; set; }
		public string PhoneNumber { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		public Role Role { get; set; }
		public string PhotoUrl { get; set; }
		public ICollection<ProExp> ProExps { get; set; }
		public ICollection<ProfileTechnology> ProfileTechnologies { get; set; }
	}
}