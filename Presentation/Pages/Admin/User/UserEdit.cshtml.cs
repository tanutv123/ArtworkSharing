using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Admin.User
{
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
            if(id == null || await _userRepository.GetUserDetail(id) == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserDetail(id);
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
                await _userRepository.UpdateUser(user);
                return RedirectToPage("./UserManagement");
            }
            return Page();
        }
    }
}
