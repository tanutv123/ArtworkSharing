using BusinessObject.Entities;
using DataAccess.Management;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
