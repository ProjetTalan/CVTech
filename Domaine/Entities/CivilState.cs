using System;
using System.ComponentModel.DataAnnotations;

namespace Domaine.Entities
{
	public class CivilState
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }
	}
}