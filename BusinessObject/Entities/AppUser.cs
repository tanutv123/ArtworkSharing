using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
	public class AppUser : IdentityUser<int>
	{
        public string Name { get; set; }
        public string Description{ get; set; }
        public int ImageId { get; set; }
        public byte Status { get; set; }

		//Navigation Property
		public List<UserFollow> FollowedUsers { get; set; }
		public List<UserFollow> IsFollowedByUsers { get; set; }
		public List<ArtworkComment> Comments { get; set; }
        public List<Transaction> TransactionSents { get; set; }
        public List<Transaction> TransactionReceived { get; set; }
		public ICollection<AppUserRole> UserRoles { get; set; }
        public List<Purchase> Purchases { get; set; }
        public Commission Commission { get; set; }
        public List<CommisionHistory> CommissionSent { get; set; }
        public List<CommisionHistory> CommissionReceived { get; set; }
        public List<ArtworkLike> LikedArtwork{ get; set; }
        public Image Image { get; set; }
    }
}
