using BusinessObject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Presentation.Pages.Admin.User
{
    [Authorize(Policy = "RequireAdminRole")]

    public class UserAddModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<AppRole> _roleManager;

        // Constructor
        public UserAddModel(IUserRepository userRepository, RoleManager<AppRole> roleManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        // Properties
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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)] public string Name { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public int SelectedRoleId { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        // OnGetAsync method
        public async Task<IActionResult> OnGetAsync()
        {
            // Populate roles in the dropdown list
            ViewData["Id"] = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
            return Page();
        }

        // OnPostAsync method
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Id"] = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
                return Page();
            }
            try
            {
                AppUser.Email = Email;
                AppUser.PhoneNumber = PhoneNumber;
                AppUser.Name = Name;
                Debug.WriteLine($"Email:{AppUser.Email}, Phone: {AppUser.PhoneNumber}, Name: {AppUser.Name}");

                var user = new AppUser
                {
                    EmailConfirmed = true,
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

                await _userRepository.AddUser(user, Password);

                user.UserRoles = new List<AppUserRole>
                {
                    new AppUserRole
                    {
                        UserId = user.Id, 
                        RoleId = SelectedRoleId
                    }
                };
                await _userRepository.UpdateUser(user);
                return RedirectToPage("/Admin/User/UserManagement");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["Id"] = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
                return Page();
            }

        }
    }
}
