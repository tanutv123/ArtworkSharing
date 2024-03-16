using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using DataAccess.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

		public UserRepository(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
		}
        public async Task<List<AppUserDTO>> GetAllUser()
        {
            var users = await UserManagement.Instance.GetAllUser();
            return users.Select(user => _mapper.Map<AppUserDTO>(user)).ToList();
        }


        public async Task<AppUserDTO> GetUserDetailAdmin(int id)
        {
            var detail = await UserManagement.Instance.GetUserDetailAdmin(id);
            return _mapper.Map<AppUserDTO>(detail);
        }

        public async Task<AppUserProfileDTO> GetUserProfile(int id)
		{
            var detail = await UserManagement.Instance.GetUserProfile(id);
			return _mapper.Map<AppUserProfileDTO>(detail);
		}

        public async Task changeUserPassword(AppUser appUser, string currentPassword, string newPassword)
        {
			try
			{
				await _userManager.ChangePasswordAsync(appUser, currentPassword, newPassword);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public async Task<IdentityResult> RegisterAsync(AppUser newUser, string password)
        {
            try
            {
                if (await IsPhoneExistAsync(newUser.PhoneNumber))
                {
                    throw new Exception("Phone number already exists.");
                }
                var result = await _userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await _userManager.SetUserNameAsync(newUser, newUser.Email);
                    await _userManager.AddToRoleAsync(newUser, "Audience");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.Status == 0 || user.EmailConfirmed == false)
            {
                return SignInResult.Failed;
            }
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
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

        public async Task<AppUser> GetUserById(int userId)
        {
            return await UserManagement.Instance.GetUserDetail(userId);
        }

        public async Task<UserDetailDTO> getUserDetail(AppUser user)
        {
            var detail = await UserManagement.Instance.getUserDetail(user);
            return _mapper.Map<UserDetailDTO>(detail);
        }

        public async Task AddUser(AppUser appUser, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user == null)
                {
                    if (await IsPhoneExistAsync(appUser.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    await _userManager.CreateAsync(appUser, password);
                    await _userManager.SetUserNameAsync(appUser, appUser.Email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUser(AppUser appUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    if (user.PhoneNumber != user.PhoneNumber && await IsPhoneExistAsync(appUser.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    user.Name = appUser.Name;
                    user.PhoneNumber = appUser.PhoneNumber;
                    user.Email = appUser.Email;
                    user.Description = appUser.Description;
                    user.UserRoles = appUser.UserRoles;
                    user.Status = appUser.Status;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    throw new Exception("User does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteUser(AppUser appUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    user.Status = 0;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    throw new Exception("User does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SignAsArtist(int userId)
        {
			try
			{
				var exitedUser = await _userManager.Users
					.Include(u => u.UserRoles)
					.FirstOrDefaultAsync(a => a.Id == userId);
				if (exitedUser != null)
				{
					exitedUser.UserRoles.Clear();
					await _userManager.AddToRoleAsync(exitedUser, "Artist");
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
    }
}
