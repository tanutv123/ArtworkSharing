using BusinessObject.Entities;

namespace Repository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll();
    }
}
