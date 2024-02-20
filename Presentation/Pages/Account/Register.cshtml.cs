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
            var image = await _userRepository.FindImage(1);

            var user = new AppUser
            {
                Email = AppUser.Email,
                PhoneNumber = AppUser.PhoneNumber,
                Name = AppUser.Name,
                UserName = AppUser.Name.ToLower(),
                ImageId = 12,
                Status = 1
            };

            var result = await _userRepository.RegisterAsync(user, Password);
            if (result.Succeeded)
            {
                return RedirectToPage("/Account/LoginPage");
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
