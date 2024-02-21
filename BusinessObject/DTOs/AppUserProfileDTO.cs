using BusinessObject.Entities;

namespace BusinessObject.DTOs
{
	public class AppUserProfileDTO
	{
        public int Name { get; set; }
        public string Description { get; set; }

        public Commission? Commission { get; set; }
    }
}
