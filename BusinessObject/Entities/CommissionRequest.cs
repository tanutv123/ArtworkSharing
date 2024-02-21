using System.Collections.Generic;

namespace BusinessObject.Entities
{
	public class CommissionRequest
	{
        public int Id { get; set; }
        public decimal ActualPrice { get; set; }
        public byte Status { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public int CommissionStatusId { get; set; }
        public CommissionStatus CommissionStatus { get; set; }
        public List<CommissionImage>? CommissionImages { get; set; }

    }
}
