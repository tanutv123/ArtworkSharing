using System;

namespace BusinessObject.Entities
{
	public class CommissionImage
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string DownloadUrl { get; set; }
		public bool isMain { get; set; }
		public string PublicId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int CommissionRequestId { get; set; }
		public CommissionRequest CommisionRequest { get; set; }
	}
}
