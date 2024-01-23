namespace DataAccess.Entities
{
	public class ArtworkLike
	{
        public int ArtworkId{ get; set; }
        public Artwork Artwork { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
