using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineShop.Pages.Users.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                Path = "/"
            });

            return RedirectToPage("/Users/Auth/Login");
        }
    }
}
