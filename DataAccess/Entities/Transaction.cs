namespace DataAccess.Entities
{
	public class Transaction
	{
        public int Id { get; set; }
        public decimal Money { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
    }
}
