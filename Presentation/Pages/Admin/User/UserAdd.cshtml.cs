using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Presentation.Pages.Admin.User
{
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
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public int SelectedRoleId { get; set; }

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
            // Check if AppUser is null and initialize if necessary
            if (AppUser == null)
            {
                AppUser = new AppUser();
            }

            // Assign values to AppUser properties
            AppUser.Email = Email;
            AppUser.PhoneNumber = PhoneNumber;
            AppUser.Name = Name;
            Debug.WriteLine($"Email:{AppUser.Email}, Phone: {AppUser.PhoneNumber}, Name: {AppUser.Name}");

            // Create a new user object
            var user = new AppUser
            {
                Email = AppUser.Email,
                PhoneNumber = AppUser.PhoneNumber,
                Name = AppUser.Name,
                UserName = AppUser.Name.ToLower(),
                Status = 1,
                UserRoles = new List<AppUserRole>
            {
                new AppUserRole
                {
                    UserId = AppUser.Id,
                    RoleId = SelectedRoleId
                }
            },
                UserImage = new UserImage
                {
                    Url = "https://media.istockphoto.com/id/1341046662/vector/picture-profile-icon-human-or-people-sign-and-symbol-for-template-design.jpg?s=612x612&w=0&k=20&c=A7z3OK0fElK3tFntKObma-3a7PyO8_2xxW0jtmjzT78=",
                    isMain = true
                }
            };

            // Add the user
            await _userRepository.AddUser(user, Password);

            // Redirect to user management page
            return RedirectToPage("/Admin/User/UserManagement");
        }
    }
}
