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


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private static UserManagement instance = null;
        private static readonly object instanceLock = new object();

        public UserManagement(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public static UserManagement GetInstance(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new UserManagement(userManager, signInManager);
                }
                return instance;
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
        public async Task AddUser(AppUser appUser, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user == null)
                {
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

        public async Task<AppUser> getUserDetail(AppUser user)
        {
            AppUser userDetailDto = null;
            try
            {
                userDetailDto = await _userManager.Users.Include(u => u.UserImage)
                    .FirstOrDefaultAsync(u => u.Email == user.Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userDetailDto;
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
