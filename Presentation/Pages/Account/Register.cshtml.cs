using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        [BindProperty]
        public AppUser AppUser { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public RegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

            }

            var user = new AppUser
            {
                Email = AppUser.Email,
                PhoneNumber = AppUser.PhoneNumber,
                Name = AppUser.Name,
                UserName = AppUser.Name.ToLower(),
                Status = 1,
                UserImage = new UserImage
                {
                    Url = "https://media.istockphoto.com/id/1341046662/vector/picture-profile-icon-human-or-people-sign-and-symbol-for-template-design.jpg?s=612x612&w=0&k=20&c=A7z3OK0fElK3tFntKObma-3a7PyO8_2xxW0jtmjzT78=",
                    isMain = true
                }
            };

            var result = await _userRepository.RegisterAsync(user, Password);
            if (result.Succeeded)
            {
                return RedirectToPage("/Account/ConfirmEmail");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return Page();
            }
        }
    }
}
