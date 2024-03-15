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
		private static GenreManagement instance = null;
		private static readonly object instanceLock = new object();

		public GenreManagement() { }

		public static GenreManagement Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new GenreManagement();
					}
					return instance;
				}
			}
		}

		public async Task<List<Genre>> GetGenresAsync()
        {
            List<Genre> genres = null;
            try
            {
                var _context = new DataContext();
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
