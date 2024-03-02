using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Repository;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public AppUser AppUser { get; set; }
        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Username can only contain letters, digits, and spaces.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public RegisterModel(IUserRepository userRepository, IEmailSender emailSender, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AppUser.Name = Name;
            AppUser.Email = Email;
            AppUser.PhoneNumber = PhoneNumber;

            var user = new AppUser
            {
                Email = AppUser.Email,
                PhoneNumber = AppUser.PhoneNumber,
                Name = AppUser.Name,
                UserName = AppUser.Email,
                Status = 1,
                UserImage = new UserImage
                {
                    Url = "https://media.istockphoto.com/id/1341046662/vector/picture-profile-icon-human-or-people-sign-and-symbol-for-template-design.jpg?s=612x612&w=0&k=20&c=A7z3OK0fElK3tFntKObma-3a7PyO8_2xxW0jtmjzT78=",
                    isMain = true
                }

            };

            var result = await _userRepository.RegisterAsync(user, Password);
            Debug.WriteLine($"EmailConfirm: {user.EmailConfirmed}");
            if (result.Succeeded)
            {
                if (user.EmailConfirmed == false)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    var email = await _userManager.GetEmailAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code },
                        protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(
                        email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    StatusMessage = "Verification email sent. Please check your email.";
                    return RedirectToPage();
                }
                else
                {
                    return RedirectToPage("/Account/LoginPage");
                }
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
