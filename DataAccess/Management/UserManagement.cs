using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DataAccess.Management
{
    public class UserManagement
    {

        /*private readonly DataContext _dataContext;
        private readonly IMapper _mapper;*/

        /*public UserManagement(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            DataContext dataContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
            _mapper = mapper;
        }*/

        private static UserManagement instance = null;
        private static readonly object instanceLock = new object();

		public static UserManagement Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new UserManagement();
					}
					return instance;
				}
			}
		}
        public IQueryable<AppUser> GetUsersAsQueryable()
        {
            var _context = new DataContext();
            return _context.Users.AsQueryable();
        }

        public async Task<AppUser> GetUserDetail(int userId)
        {
            AppUser userDetailDto = null;
            try
            {
                var _dataContext = new DataContext();

                userDetailDto = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userDetailDto;
        }

        public async Task<AppUser> GetUserProfile(int id)
        {
            AppUser result = null;

            try
            {
                var _dataContext = new DataContext();
                result = await _dataContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public async Task<List<AppUser>> GetAllUser()
        {
            List<AppUser> appUsers = null;
            try
            {
                var _dataContext = new DataContext();
                appUsers = await _dataContext.Users
                    .Include(a => a.UserRoles)
                    .ThenInclude(a => a.Role)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return appUsers;
        }

        public async Task<AppUser> GetUserDetailAdmin(int id)
        {
            AppUser user = null;

            try
            {
                var _dataContext = new DataContext();
                user = await _dataContext.Users.Where(x => x.Id == id).Include(u => u.UserRoles).ThenInclude(u => u.Role).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public async Task<AppUser> getUserDetail(AppUser user)
        {
            AppUser userDetailDto = null;
            try
            {
                var _dataContext = new DataContext();
                userDetailDto = await _dataContext.Users
                    .Include(u => u.UserImage)
                    .FirstOrDefaultAsync(u => u.Email == user.Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userDetailDto;
        }
    }
    
    
}
