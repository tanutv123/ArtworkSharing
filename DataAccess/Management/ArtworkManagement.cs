using BusinessObject.Entities;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Management
{
    public class ArtworkManagement
    {
        private readonly DataContext _dataContext;

        public IEnumerable<Artwork> GetArtworks()
        {
            IList<Artwork> artworks = new List<Artwork>();
            artworks = _dataContext.Artworks.ToList();
            return artworks;
        }
    }
}
