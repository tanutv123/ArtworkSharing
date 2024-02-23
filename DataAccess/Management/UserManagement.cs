﻿using AutoMapper;
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
            if (await IsPhoneExistAsync(newUser.PhoneNumber))
            {
                throw new Exception("Phone number already exists.");
            }
            newUser.UserName = newUser.Email;
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

        public async Task<List<AppUserDTO>> GetAllUser()
        {
            List<AppUserDTO> appUsers = null;
            try
            {
                appUsers = await _dataContext.Users
                    .Include(a => a.UserRoles)
                    .ProjectTo<AppUserDTO>(_mapper.ConfigurationProvider).ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return appUsers;
        }

        public async Task<AppUserDTO> GetUserDetail(int id)
        {
            AppUserDTO user = null;

            try
            {
                user = await _dataContext.Users.Where(x => x.Id == id).ProjectTo<AppUserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task UpdateUser(AppUser appUser)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
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
                if(user != null)
                {
                    user.Status = 0; 

/*                    if (user.Status == 0)
                    {
                        user.SecurityStamp = Guid.NewGuid().ToString();
                    }*/
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    throw new Exception("User does not exist");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
