using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Presentation.Pages.Account
{
    [AllowAnonymous]
    public class LoginPageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public AppUser AppUser { get; set; }
        [BindProperty]
        [Required]
        [EmailAddress]
        public string EmailAddress {  get; set; }

        [BindProperty]
        [Required]
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
            AppUser.Email = EmailAddress;
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
                    return RedirectToPage("/Admin/Purchase/PurchaseList");
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
