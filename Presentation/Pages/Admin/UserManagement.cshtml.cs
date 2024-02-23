using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin
{
    [Authorize(Policy = "RequireAdminRole")]
    public class UserManagementModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public UserManagementModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IList<AppUserDTO> AppUserDTO { get; set; }

        public async Task OnGetAsync()
        {
            var users = await _userRepository.GetAllUser();

            AppUserDTO = users.ToList();
        }
    }
}
