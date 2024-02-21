using BusinessObject.Entities;
using DataAccess.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.User;

public class Detail : PageModel
{
    [BindProperty] private AppUser AppUser { get; set; }
    private readonly IUserRepository _userRepository;
    public Detail(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    
    public void OnGet(int userId)
    {
        
    }
}