using BusinessObject.Entities;
using DataAccess.Management;

namespace Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly GenreManagement _genreManagement;

        public GenreRepository(GenreManagement genreManagement)
        {
            _genreManagement = genreManagement;
        }
        public async Task<List<Genre>> GetAll()
        {
            return await _genreManagement.GetGenresAsync();
        }
    }
}
