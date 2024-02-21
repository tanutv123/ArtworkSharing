using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
	public class AddCommisionDTO
	{
		public int Id { get; set; }
		[Required]
		public decimal TargetPrice { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public int AppUserId { get; set; }
    }
}
