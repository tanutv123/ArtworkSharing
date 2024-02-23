using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(AppUser newUser, string password);
        Task<SignInResult> LoginAsync(string email, string password);
        Task SignOutAsync();
        Task<AppUserProfileDTO> GetUserProfile(int id);
        Task<List<AppUserDTO>> GetAllUser();
        Task<AppUserDTO> GetUserDetail(int id);
        Task UpdateUser(AppUser appUser);
        Task DeleteUser(AppUser appUser);
    }
}
