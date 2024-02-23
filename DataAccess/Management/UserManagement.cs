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

namespace DataAccess.Management
{
    public class UserManagement
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _dataContext;
		private readonly IMapper _mapper;

		public UserManagement(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            DataContext dataContext, 
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
			_mapper = mapper;
		}

        public async Task<IdentityResult> RegisterAsync(AppUser newUser, string password)
        {
            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Audience");
            }
            return result;
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return SignInResult.Failed;
            }
            var result = await _signInManager.PasswordSignInAsync(email, password, false,lockoutOnFailure: false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user != null;
        }

        public async Task<AppUser> GetUserDetail(int userId)
        {
            AppUser userDetailDto = null;
            try
            {
                userDetailDto = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userDetailDto;
        }

        public async Task<AppUserProfileDTO> GetUserProfile(int id)
        {
            AppUserProfileDTO result = null;

            try
            {
				result = await _dataContext.Users.Where(x => x.Id == id).ProjectTo<AppUserProfileDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> ChangeUserPassword(AppUser user, string currentPass, string newPass)
        {
            try
            {
                await _userManager.ChangePasswordAsync(user, currentPass, newPass);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
