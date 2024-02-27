using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public ArtworkManagement(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Artwork>> GetArtworksAsync()
        {
            IList<Artwork> artworks = new List<Artwork>();
            if (_dataContext != null && _dataContext.Artworks != null)
            {
                artworks = await _dataContext.Artworks
                    .Include(a => a.ArtworkImage)
                    .Include(a => a.Genre)
                    .Include(a => a.AppUser)
                    .ToListAsync();
            }
            return artworks;
        }

        public async Task<bool> LikeArtworkAsync(int userId, int artworkId)
        {
            var existingLike = await _dataContext.ArtworkLikes.AnyAsync(x => x.ArtworkId == artworkId && x.AppUserId == userId);
            var like = new ArtworkLike
            {
                AppUserId = userId,
                ArtworkId = artworkId
            };
            if (existingLike)
            {
                _dataContext.ArtworkLikes.Remove(like);
                await _dataContext.SaveChangesAsync();
                return false;
            }
            _dataContext.ArtworkLikes.Add(like);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FollowUserAsync(int targetuserid, int sourceuserid)
        {
            var existingFollow = await _dataContext.UserFollows.Where(x => x.TargetUserId == targetuserid && x.SourceUserId == sourceuserid).ToListAsync();

            
            var userfollow = new UserFollow
            {
                SourceUserId = sourceuserid,
                TargetUserId = targetuserid
            };
            if (!existingFollow.IsNullOrEmpty())
            {
                UserFollow userFollow = new UserFollow();
                userFollow = await _dataContext.UserFollows.Where(x => x.TargetUserId == targetuserid && x.SourceUserId == sourceuserid).FirstOrDefaultAsync();

                _dataContext.UserFollows.Remove(userFollow);
                await _dataContext.SaveChangesAsync();
                return false;
            }
            _dataContext.UserFollows.Add(userfollow);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<Artwork> GetArtworkAsync (int artworkid)
        {
            Artwork artwork = new Artwork();
            if (_dataContext != null && _dataContext.Artworks != null)
            {
                artwork = await _dataContext.Artworks.Include(a => a.Genre).Include(a => a.ArtworkImage).FirstOrDefaultAsync(a => a.Id == artworkid);
            }
            return artwork;
        }

        public async Task<IEnumerable<ArtworkComment>> GetCommentsByArtworkId (int artworkid)
        {
            IList<ArtworkComment> comment = new List<ArtworkComment>();
            comment = await _dataContext.ArtworkComments.Where(a => a.ArtworkId == artworkid).Include(a => a.Artwork).Include(a => a.AppUser).ToListAsync();

            return comment;
        }

        public async Task ArtworkCommentAsync(int artworkid, int appuserid, string content, DateTime createdDate)
        {
            ArtworkComment comment = new ArtworkComment
            {
                AppUserId = appuserid,
                ArtworkId = artworkid,
                Content = content,
                CreatedDate = createdDate
            };
            _dataContext.ArtworkComments.Add(comment);
            await _dataContext.SaveChangesAsync();
        }

            public async Task<IEnumerable<Artwork>> GetArtworkBySearchString(string title)
            {
                IList<Artwork> artworks = new List<Artwork>();
                try
                {
                    if (_dataContext != null && _dataContext.Artworks != null)
                    artworks = await _dataContext.Artworks
                        .Include(a => a.Genre)
                        .Include(a => a.AppUser)
                        .Include(a => a.ArtworkImage).Where(a => a.Title.Contains(title))
                        .ToListAsync();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return artworks;
            }
    }
}
