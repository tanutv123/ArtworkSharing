using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;

namespace Presentation.Pages.Admin.User
{
    [Authorize(Policy = "RequireAdminRole")]
    public class UserEditModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserEditModel(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public AppUserDTO AppUserDTO { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if(id == null || await _userRepository.GetUserDetailAdmin(id) == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserDetailAdmin(id);
            if (user == null)
            {
                return NotFound();
            }
            AppUserDTO = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var user = _mapper.Map<BusinessObject.Entities.AppUser>(AppUserDTO);
                await _userRepository.UpdateUser(user);
                return RedirectToPage("./UserManagement");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
