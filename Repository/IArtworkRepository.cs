using BusinessObject.DTOs;
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
        Task DeleteArtwork(int artworkid);
        Task UpdateArtwork(Artwork artwork);
        Task UpdateArtworkImage(ArtworkImage image);
        Task<IEnumerable<Artwork>> GetPaginatedResult(int currentPage, int pageSize = 10);
        Task<int> GetCount();
        Task<Artwork> GetArtworksAsyncWithLike(int userId, int artworkId);
        Task<bool> HasUserLikedArtwork(int userId, int artworkId);
        Task BuyArtwork(AddPurchaseDTO addPurchase, AddTransationDTO addTransationDTO);
        Task<bool> HasUserFollowed(int sourceId, int targetId);
    }

}
