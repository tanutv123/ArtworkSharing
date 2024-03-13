using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.DTOs;
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
        /*private readonly DataContext _dataContext;
        private readonly IMapper _mapper;*/

        private static ArtworkManagement instance = null;
        private static readonly object instanceLock = new object();

        public ArtworkManagement() { }

        public static ArtworkManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ArtworkManagement();
                    }
                    return instance;
                }
            }
        }

        /*public ArtworkManagement(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }*/

        public async Task<IEnumerable<Artwork>> GetArtworksAsync()
        {
            try
            {
                var _dataContext = new DataContext();
                IEnumerable<Artwork> artworks = new List<Artwork>();
                if (_dataContext != null && _dataContext.Artworks != null)
                {
                    artworks = await _dataContext.Artworks
                        .Include(a => a.ArtworkImage)
                        .Include(a => a.Genre)
                        .Include(a => a.AppUser)
                        .Where(a => a.Status == 1)
                        .ToListAsync();
                }
                
                return artworks;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> LikeArtworkAsync(int userId, int artworkId)
        {
            var _dataContext = new DataContext();
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
            var _dataContext = new DataContext();
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
            try
            {
                var _dataContext = new DataContext();
                Artwork artwork = new Artwork();
                if (_dataContext != null && _dataContext.Artworks != null)
                {
                    artwork = await _dataContext.Artworks.Include(a => a.Genre).Include(a => a.ArtworkImage).Include(a => a.AppUser).FirstOrDefaultAsync(a => a.Id == artworkid);
                }
                return artwork;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ArtworkComment>> GetCommentsByArtworkId (int artworkid)
        {
            try
            {
                var _dataContext = new DataContext();
                IList<ArtworkComment> comment = new List<ArtworkComment>();
                comment = await _dataContext.ArtworkComments.Where(a => a.ArtworkId == artworkid).Include(a => a.Artwork).Include(a => a.AppUser).ToListAsync();

                return comment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task ArtworkCommentAsync(int artworkid, int appuserid, string content, DateTime createdDate)
        {
            try
            {
                var _dataContext = new DataContext();
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

            public async Task<IEnumerable<Artwork>> GetArtworkBySearchString(string title)
            {
            
                try
                {
                    var _dataContext = new DataContext();
                    IList<Artwork> artworks = new List<Artwork>();
                    if (_dataContext != null && _dataContext.Artworks != null)
                    artworks = await _dataContext.Artworks
                        .Include(a => a.Genre)
                        .Include(a => a.AppUser)
                        .Include(a => a.ArtworkImage).Where(a => a.Title.Contains(title))
                        .ToListAsync();

                    return artworks;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                
            }
        public async Task<IEnumerable<Artwork>> GetArtworkListByArtistId(int artistid)
        {
            try
            {
                var _dataContext = new DataContext();
                IList<Artwork> artworks = new List<Artwork>();
                if (_dataContext != null && _dataContext.Artworks != null)
                {
                    artworks = await _dataContext.Artworks
                        .Include(a => a.ArtworkImage)
                        .Include(a => a.Genre)
                        .Include(a => a.AppUser).Where(a => a.AppUserId == artistid)
                        .ToListAsync();
                }
                return artworks;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddArtwork(Artwork artwork)
        {
            try
            {
                var _dataContext = new DataContext();
                artwork.Status = 1;
                artwork.CreatedDate = DateTime.UtcNow;
                await _dataContext.Artworks.AddAsync(artwork);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /*public async Task<AddArtworkImageDTO> GetSingleAddArtworkImage(int id)
        {
            

            try
            {
                var _dataContext = new DataContext();
                var _mapper = IMapper();
                AddArtworkImageDTO result = null;
                result = await _dataContext.Artworks.Where(x => x.Id == id).ProjectTo<AddArtworkImageDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }*/
        public async Task AddArtworkImage(ArtworkImage artworkImage)
        {
            try
            {
                var _dataContext = new DataContext();
                var art = await _dataContext.Artworks.SingleOrDefaultAsync(x => x.Id == artworkImage.ArtworkId);
                if (art != null)
                {
                    art.Status = 1;
                    _dataContext.ArtworkImages.Add(artworkImage);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Artwork not found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public async Task UpdateArtwork(Artwork artwork)
		{
			try
			{
                var _dataContext = new DataContext();
                var dbArtwork = await _dataContext.Artworks.FindAsync(artwork.Id);
				dbArtwork.Description = artwork.Description;
                dbArtwork.GenreId = artwork.GenreId;
                dbArtwork.Price = artwork.Price;

				await _dataContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task DeleteArtwork(int artworkid)
		{
			try
			{
                var _dataContext = new DataContext();
                var dbArtwork = await _dataContext.Artworks.FindAsync(artworkid);
				dbArtwork.Status = 0;
				await _dataContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task DeleteArtworkImage(ArtworkImage artworkImage)
		{
			try
			{
				//Nothing Here
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task UpdateArtworkImage(ArtworkImage artworkImage)
		{
			try
			{
                var _dataContext = new DataContext();
				var art = await _dataContext.Artworks.SingleOrDefaultAsync(x => x.Id == artworkImage.ArtworkId);
                var artimg = await _dataContext.ArtworkImages.SingleOrDefaultAsync(x => x.Id == artworkImage.Id);
				if (art != null && artimg != null)
				{
					artworkImage.Url = artimg.Url;
                    artworkImage.PublicId = artimg.PublicId;

					await _dataContext.SaveChangesAsync();
				}
				else
				{
					throw new Exception("Artwork not found!");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
        public async Task<bool> HasUserLikedArtwork(int userId, int artworkId)
        {
            try
            {
                var _context = new DataContext();
                return await _context.ArtworkLikes.AnyAsync(a => a.AppUserId == userId && a.ArtworkId == artworkId);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task BuyArtwork (Purchase purchase, Transaction transaction)
        {
            try
            {
                var _context = new DataContext();
                var isPurchase = await _context.Purchases.AnyAsync(x => x.ArtworkId == purchase.ArtworkId && x.AppUserId == purchase.AppUserId);
                if (!isPurchase)
                {
                    await _context.Purchases.AddAsync(purchase);
                    await _context.SaveChangesAsync();
                    await _context.Transactions.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                }
                
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> HasUserFollowed(int sourceId, int targetId)
        {
            try
            {
                var _context = new DataContext();
                return await _context.UserFollows.AnyAsync(a => a.SourceUserId == sourceId && a.TargetUserId == targetId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
