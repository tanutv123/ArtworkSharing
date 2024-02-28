using BusinessObject.Entities;

namespace BusinessObject.DTOs
{
	public class AppUserProfileDTO
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Commission? Commission { get; set; }
    }
}
