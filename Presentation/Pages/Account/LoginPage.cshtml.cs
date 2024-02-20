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

        [BindProperty]
        public string EmailAddress { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginPageModel(IUserRepository userRepository)
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
            Debug.WriteLine($"EmailAddress = {EmailAddress}, Password = {Password}");
            var result = await _userRepository.LoginAsync(EmailAddress, Password);

            if (result.Succeeded)
            {
                HttpContext.Session.SetString("EmailAddress", EmailAddress);
                return RedirectToPage("/Home/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
}
