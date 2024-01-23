namespace DataAccess.Entities
{
	public class Purchase
	{
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ArtworkId { get; set; }
        public Artwork Artwork { get; set; }

    }
}
