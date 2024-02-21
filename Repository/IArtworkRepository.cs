using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IArtworkRepository
    {
        IEnumerable<Artwork> GetArtworks();
    }
}
