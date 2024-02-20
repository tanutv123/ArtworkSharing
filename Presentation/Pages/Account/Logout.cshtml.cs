using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Presentation.Pages.Account
{
    public class LogoutModel : PageModel
    {

        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Account/LoginPage");
        }
    }

}
