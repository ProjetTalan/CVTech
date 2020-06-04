﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
	public class CivilStateModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:MM / dd / yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfBirth { get; set; }
	}
}