using System.Collections.Generic;

namespace Domaine.Entities
{
	public class Technology
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ICollection<ProExp> ProExps { get; set; }
		public ICollection<ProfileTechnology> ProfileTechnologies { get; set; }
	}
}