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

        public UserManagement(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
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
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
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
    }
}
