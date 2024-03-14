using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BusinessObject.DTOs;
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
        private readonly IMapper _mapper;
        public ArtworkRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task AddArtworkComment(int artworkid, int commentid, string content, DateTime createdDate)
        {
            await ArtworkManagement.Instance.ArtworkCommentAsync(artworkid, commentid, content, createdDate);
        }

        public async Task<IEnumerable<Artwork>> GetArtworkByTitle(string title)
            => await ArtworkManagement.Instance.GetArtworkBySearchString(title);

        public async Task<bool> FollowArtist(int sourceid, int followerid)
        {
            return await ArtworkManagement.Instance.FollowUserAsync(sourceid, followerid);
        }

        public async Task<Artwork> GetArtworkById(int artworkid)
        {
            return await ArtworkManagement.Instance.GetArtworkAsync(artworkid);
        }

        public async Task<IEnumerable<ArtworkComment>> GetArtworkComments(int artworkid)
        {
            return await ArtworkManagement.Instance.GetCommentsByArtworkId(artworkid);
        }

        public async Task<IEnumerable<Artwork>> GetArtworks()
        {
            return await ArtworkManagement.Instance.GetArtworksAsync();
        }

        public async Task<bool> LikeArtwork(int userid, int artworkid)
        {
            return await ArtworkManagement.Instance.LikeArtworkAsync(userid, artworkid);
        }
        public async Task<IEnumerable<Artwork>> GetArtworksByUserId(int artistid)
        {
            return await ArtworkManagement.Instance.GetArtworkListByArtistId(artistid);
        }
        public async Task AddArtwork(Artwork artwork)
        {
            await ArtworkManagement.Instance.AddArtwork(artwork);
        }
        public async Task AddArtworkImage(ArtworkImage artworkImage)
        {
            await ArtworkManagement.Instance.AddArtworkImage(artworkImage);
        }
        public async Task DeleteArtwork(int artworkid)
        {
            await ArtworkManagement.Instance.DeleteArtwork(artworkid);
        }
        public async Task UpdateArtwork(Artwork artwork)
        {
            await ArtworkManagement.Instance.UpdateArtwork(artwork);
        }
        public async Task UpdateArtworkImage(ArtworkImage image)
        {
            await ArtworkManagement.Instance.UpdateArtworkImage(image);
        }
        public async Task<IEnumerable<Artwork>> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = await ArtworkManagement.Instance.GetArtworksAsync();
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await ArtworkManagement.Instance.GetArtworksAsync();
            return data.Count();
        }

        public async Task<Artwork> GetArtworksAsyncWithLike(int userId, int artworkId)
        {
            var artwork = await ArtworkManagement.Instance.GetArtworkAsync(artworkId);
            return artwork;
        }

        public async Task<bool> HasUserLikedArtwork(int userId, int artworkId)
        {
            return await ArtworkManagement.Instance.HasUserLikedArtwork(userId, artworkId);    
        }

        public async Task<bool> HasUserFollowed(int sourceId, int targetId)
        {
            return await ArtworkManagement.Instance.HasUserFollowed(sourceId, targetId);
        }
        public async Task BuyArtwork(AddPurchaseDTO addPurchase, AddTransationDTO addTransationDTO)
        {
            var purchase = _mapper.Map<Purchase>(addPurchase);
            var trans = _mapper.Map<Transaction>(addTransationDTO);
            await ArtworkManagement.Instance.BuyArtwork(purchase, trans);
        }

        public async Task<IList<Artwork>> GetArtworkAdmin()
        {
            return await ArtworkManagement.Instance.GetAllArtworkAdmin();
        }

        public async Task<bool> ChangeArtworkStatusAdmin(int artworkId)
        {
            return await ArtworkManagement.Instance.ChangeArtworkStatusAdmin(artworkId);
        }
    }
}
