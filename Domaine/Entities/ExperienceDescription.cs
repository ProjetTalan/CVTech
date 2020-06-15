namespace Domaine.Entities
{
	public class ExperienceDescription
	{
		public int Id { get; set; }
		public int ProExpId { get; set; }
		public int PositionId { get; set; }
		public Position Position { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}