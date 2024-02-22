using BusinessObject.Entities;

namespace Repository
{
	public interface ICommissionRepository
	{
		Task<Commission> GetArtistCommission(int id);
		Task<bool> CheckArtistRegisterCommission(int id);
		Task AddCommission(Commission commission);
	}
}
