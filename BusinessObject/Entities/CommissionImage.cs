namespace BusinessObject.Entities
{
	public class CommissionImage
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public bool isMain { get; set; }
		public string PublicId { get; set; }
		public int CommissionHistoryId { get; set; }
		public CommissionRequest CommisionHistory { get; set; }
	}
}
