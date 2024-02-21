using BusinessObject.Entities;

namespace Repository
{
	public interface ICommissionRepository
	{
		Task<Commission> GetArtistCommission(int id);
	}
}
