using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.User
{
    public class UserDetailModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public UserDetailModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AppUserDTO AppUserDTO { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserDetail(id);
            if(user != null)
            {
                AppUserDTO = user;
            }

            return Page();
        }
    }
}
