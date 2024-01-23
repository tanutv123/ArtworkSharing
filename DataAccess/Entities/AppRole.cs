using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
	public class AppRole : IdentityRole<int>
	{
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
