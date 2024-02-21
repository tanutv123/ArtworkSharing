using BusinessObject.Entities;
using DataAccess.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.User;

public class Detail : PageModel
{
    private readonly IUserRepository _userRepository;
    
    public Detail(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [BindProperty] 
    public AppUser AppUser { get; set; }

    public async void OnGet(int userId)
    {
        AppUser = await _userRepository.GetUserById(userId);
        
    }
}