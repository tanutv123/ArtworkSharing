using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
	public class CommissionRequestDTO
	{
        [Required]
        public decimal ActualPrice { get; set; }
        [Required]
        public string RequestDescription { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public int CommissionStatusId { get; set; }
    }
}
