namespace BusinessObject.DTOs
{
	public class CommissionRequestDTO
	{
        public decimal ActualPrice { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CommissionStatusId { get; set; }
    }
}
