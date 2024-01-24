namespace DataAccess.Entities
{
	public class Artwork
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AppUserId { get; set; }
		public int ImageId { get; set; }

		public byte Status { get; set; }

        //Navigation Property
        public List<ArtworkLike> Likes { get; set; }
        public AppUser AppUser { get; set; }
        public Genre Genre { get; set; }
        public Image Image{ get; set; }
        public List<ArtworkComment> Comments{ get; set; }
        public List<Purchase> Purchases { get; set; }

    }
}
