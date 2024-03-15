using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using DataAccess.Data;
using DataAccess.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.User;
[Authorize]
public class Detail : PageModel
{
    private readonly IUserRepository _userRepository;
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private DataContext _dataContext;
    public Detail(IUserRepository userRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext dataContext)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _dataContext = dataContext;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    [BindProperty]
    public string image { get; set; }
    [BindProperty] 
    public AppUser AppUser { get; set; }
    [TempData]
    public string StatusMessage { get; set; }
    [BindProperty]
    public string Username { get; set; }
    public class InputModel
    {
        public string FullName { get; set; }
        public String ImageURL { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    
    public UserDetailDTO dto { get; set; }

    private async Task LoadAsync(AppUser appUser)
    {
        var userName = await _userRepository.GetUserDetailAdmin(appUser.Id);
        var userD = await _userRepository.getUserDetail(appUser);
        
        var userPhone = await _userManager.GetPhoneNumberAsync(appUser);
        Username = userName.Email;
        Input = new InputModel
        {
            FullName = userName.Name,
            ImageURL = userD.userImageUrl,
            Name = userName.Email,
            PhoneNumber = userPhone,
            Description = appUser.Description
        };
        
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Can not load user ID = '{_userManager.GetUserId(User)}'");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostUpdate()
     {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Can not load user ID: ''{_userManager.GetUserId(User)}");
        }

        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (Input.PhoneNumber != phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                StatusMessage = "Error in updating Phone Number!";
                return RedirectToPage();
            }
        }
        // user.UserImage = Input.UserImage;
        user.Name = Input.FullName;
        // user.UserName = Username;
        user.Description = Input.Description;
        await _userManager.UpdateAsync(user);
            
        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your information have been updated!";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostSign()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userRepository.SignAsArtist(user.Id);
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "You has become an Artist! Be creative !";
                return RedirectToPage();
            }
            else
            {
                {
                    return NotFound($"Can not load user ID: ''{_userManager.GetUserId(User)}");
                }
            }
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    
   
}