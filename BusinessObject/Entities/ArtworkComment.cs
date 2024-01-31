using System;

namespace BusinessObject.Entities
{
	public class ArtworkComment
	{
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ArtworkId { get; set; }
        public Artwork Artwork { get; set; }
    }
}
