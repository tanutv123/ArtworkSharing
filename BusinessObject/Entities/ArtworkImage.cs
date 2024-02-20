namespace BusinessObject.Entities
{
	public class ArtworkImage
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public bool isMain { get; set; }
		public string PublicId { get; set; }
		public int ArtworkId { get; set; }
		public Artwork Artwork { get; set; }
	}
}
