using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
	public class CommissionRequestHistoryDTO
	{
		public int Id { get; set; }
		public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string RequestDescription { get; set; }
        public string NotAcceptedReason { get; set; }
        public byte Status { get; set; }
		public string SenderEmail { get; set; }
		public string ReceiverEmail { get; set; }
		public string CommissionStatusDescription { get; set; }
		public string GenreName { get; set; }
		public DateTime RequestDate { get; set; }
		public DateTime DueDate { get; set; }
        public List<CommissionImage>? CommissionImages{ get; set; }
    }
}
