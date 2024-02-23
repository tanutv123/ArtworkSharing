using BusinessObject.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
	public class AddCommissionImageDTO
	{
		[Required]
		public string Url { get; set; }
		public bool isMain { get; set; }
		[Required]
		public string PublicId { get; set; }
		public DateTime CreatedDate { get; set; }
		[Required]
		public int CommissionRequestId { get; set; }
	}
}
