using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domaine.Entities
{
	public class ProfileTechnology
	{
		[Key]
		[Column(Order = 0)]
		public int ProfileId { get; set; }
		[Key]
		[Column(Order = 1)]
		public int TechnologyId { get; set; }
		public Technology Technology { get; set; }
		public int TechLevelId { get; set; }
	}
}