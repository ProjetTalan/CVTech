namespace Application.Models
{
	public class ProfileTechModel
	{
		public int ProfileId { get; set; }
		public int TechnologyId { get; set; }
		public string TechnologyName { get; set; }
		public int TechLevelId { get; set; }
		public string TechLevelDescription { get; set; }
	}
}