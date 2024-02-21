namespace BusinessObject.DTOs
{
	public class AddCommisionDTO
	{
		public int Id { get; set; }
		public decimal TargetPrice { get; set; }
		public string Description { get; set; }
		public int AppUserId { get; set; }
	}
}
