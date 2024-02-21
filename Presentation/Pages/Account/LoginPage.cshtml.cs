using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Presentation.Pages.Account
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public AppUser AppUser { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginPageModel(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

            }
            string email = AppUser.Email;
            Debug.WriteLine($"EmailAddress = {email}, Password = {Password}");
            var result = await _userRepository.LoginAsync(email, Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault(); 

                HttpContext.Session.SetString("EmailAddress", email);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetInt32("Id", user.Id);

                if (role.Equals("Audience") || role.Equals("Artist"))
                {
                    return RedirectToPage("/Home/Index");
                }
                if (role.Equals("Admin") || role.Equals("Manager"))
                {
                    return RedirectToPage("/Admin/Index");
                }
                else
                {
                    return RedirectToPage("/Account/AccessDenied");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
}
