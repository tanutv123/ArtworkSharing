namespace DataAccess.Entities
{
	public class CommisionHistory
	{
        public int Id { get; set; }
        public decimal ActualPrice { get; set; }
        public byte Status { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

    }
}
