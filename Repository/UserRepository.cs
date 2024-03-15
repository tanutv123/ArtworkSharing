using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using DataAccess.Management;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagement _userManagement;

		public UserRepository(UserManagement userManagement)
        {
            _userManagement = userManagement;
		}

        public async Task DeleteUser(AppUser appUser)
        {
            await _userManagement.DeleteUser(appUser);
        }

        public async Task<List<AppUserDTO>> GetAllUser()
        {
            return await _userManagement.GetAllUser();
        }

        public async Task<AppUserDTO> GetUserDetailAdmin(int id)
        {
            return await _userManagement.GetUserDetailAdmin(id);
        }

        public async Task<AppUserProfileDTO> GetUserProfile(int id)
		{
			return await _userManagement.GetUserProfile(id);
		}

        public async Task changeUserPassword(AppUser appUser, string currentPassword, string newPassword)
        {
            try
            {
                await _userManagement.ChangeUserPassword(appUser, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            return await _userManagement.LoginAsync(email, password);
        }

        public async Task<IdentityResult> RegisterAsync(AppUser newUser, string password)
        {
            return await _userManagement.RegisterAsync(newUser, password);
        }

        public async Task SignOutAsync()
        {
            await _userManagement.SignOutAsync();
        }

        public async Task UpdateUser(AppUser appUser)
        {
            await _userManagement.UpdateUser(appUser);
        }
        public async Task<AppUser> GetUserById(int userId)
        {
            return await _userManagement.GetUserDetail(userId);
        }

        public async Task AddUser(AppUser appUser, string password)
        {
            await _userManagement.AddUser(appUser, password);
        }
        public async Task<UserDetailDTO> getUserDetail(AppUser user)
        {
            return await _userManagement.getUserDetail(user);
        }

        public async Task<bool> SignAsArtist(int userId)
        {
            return await _userManagement.SignAsArtist(userId);
        }
    }
}
