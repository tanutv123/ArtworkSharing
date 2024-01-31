namespace BusinessObject.Entities
{
	public class Image
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public bool isMain { get; set; }
		public string PublicId { get; set; }

        public AppUser AppUser { get; set; }
        public Artwork Artwork { get; set; }
    }
}
