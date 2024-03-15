using BusinessObject.Entities;
using DataAccess.Management;

namespace Repository
{
    public class GenreRepository : IGenreRepository
    {

        public GenreRepository()
        {
        }
        public async Task<List<Genre>> GetAll()
        {
            return await GenreManagement.Instance.GetGenresAsync();
        }
    }
}
