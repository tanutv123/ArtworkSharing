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
        Task<IEnumerable<Artwork>> GetArtworks();
        Task<bool> LikeArtwork(int userid, int artworkid);
        Task<bool> FollowArtist(int sourceid, int followerid);
        Task<Artwork> GetArtworkById(int artworkid);
        Task<IEnumerable<ArtworkComment>> GetArtworkComments(int artworkid);
        Task AddArtworkComment(int artworkid, int  commentid, string content, DateTime createdDate);
        Task<IEnumerable<Artwork>> GetArtworkByTitle(string title);
        Task<IEnumerable<Artwork>> GetArtworksByUserId(int artistid);
        Task AddArtwork(Artwork artwork);
        Task AddArtworkImage(ArtworkImage artworkImage);
    }

}
