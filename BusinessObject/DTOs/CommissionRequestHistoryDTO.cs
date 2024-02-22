using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
	public class CommissionRequestHistoryDTO
	{
		public int Id { get; set; }
		public decimal ActualPrice { get; set; }
		public string RequestDescription { get; set; }
		public string SenderEmail { get; set; }
		public string ReceiverEmail { get; set; }
		public string CommissionStatusDescription { get; set; }
		public DateTime RequestDate { get; set; }
        public List<CommissionImage>? CommissionImages{ get; set; }
    }
}
