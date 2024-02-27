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

        public async Task AddArtworkComment(int artworkid, int commentid, string content, DateTime createdDate)
        {
            await _artworkManagement.ArtworkCommentAsync(artworkid, commentid, content, createdDate);
        }

        public async Task<IEnumerable<Artwork>> GetArtworkByTitle(string title)
            => await _artworkManagement.GetArtworkBySearchString(title);

        public async Task<bool> FollowArtist(int sourceid, int followerid)
        {
            return await _artworkManagement.FollowUserAsync(sourceid, followerid);
        }

        public async Task<Artwork> GetArtworkById(int artworkid)
        {
            return await _artworkManagement.GetArtworkAsync(artworkid);
        }

        public async Task<IEnumerable<ArtworkComment>> GetArtworkComments(int artworkid)
        {
            return await _artworkManagement.GetCommentsByArtworkId(artworkid);
        }

        public async Task<IEnumerable<Artwork>> GetArtworks()
        {
            return await _artworkManagement.GetArtworksAsync();
        }

        public async Task<bool> LikeArtwork(int userid, int artworkid)
        {
            return await _artworkManagement.LikeArtworkAsync(userid, artworkid);
        }
    }
}
