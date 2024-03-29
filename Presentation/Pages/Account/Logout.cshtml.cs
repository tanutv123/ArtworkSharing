using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Account
{
    public class LogoutModel : PageModel
    {
		private readonly IUserRepository _userRepository;

		public LogoutModel(IUserRepository userRepository)
        {
			_userRepository = userRepository;
		}
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Clear();
            await _userRepository.SignOutAsync();
            return RedirectToPage("/Account/LoginPage");
        }
    }

}
