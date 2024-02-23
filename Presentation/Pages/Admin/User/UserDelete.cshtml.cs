using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Diagnostics;

namespace Presentation.Pages.Admin.User
{
    public class UserDeleteModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserDeleteModel(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public AppUserDTO AppUserDTO { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || await _userRepository.GetUserDetailAdmin(id) == null)
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
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<BusinessObject.Entities.AppUser>(AppUserDTO);
                Debug.WriteLine($"Email: {user.Email}");
                await _userRepository.DeleteUser(user);
                return RedirectToPage("./UserManagement");
            }
            return Page();
        }
    }
}
