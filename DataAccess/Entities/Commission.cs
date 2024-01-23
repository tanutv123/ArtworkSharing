namespace DataAccess.Entities
{
	public class Commission
	{
        public int Id { get; set; }
        public decimal TargetPrice { get; set; }
        public string Description { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
