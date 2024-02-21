using BusinessObject.Entities;
using DataAccess.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtworkManagement _artworkManagement;

        public ArtworkRepository(ArtworkManagement artworkManagement)
        {
            _artworkManagement = artworkManagement;
        }

        public IEnumerable<Artwork> GetArtworks()
        {
            return _artworkManagement.GetArtworks();
        }
    }
}
