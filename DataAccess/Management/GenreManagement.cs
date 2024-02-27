using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Management
{
    public class GenreManagement
    {
        private readonly DataContext _context;

        public GenreManagement(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            List<Genre> genres = null;
            try
            {
                genres = await _context.Genres.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return genres;
        }
    }
}
