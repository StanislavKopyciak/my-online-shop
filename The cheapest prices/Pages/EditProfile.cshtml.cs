using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using The_cheapest_prices.Pages.Data;

namespace The_cheapest_prices.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly UserRepository _userRepository;

        public EditProfileModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public User CurrentUser { get; set; } 
        

        public void OnGet()
        {
            int userId = int.Parse(HttpContext.User.FindFirst("UserId")?.Value ?? "0");
            CurrentUser = _userRepository.GetUserById(userId);  
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int userId = int.Parse(HttpContext.User.FindFirst("UserId")?.Value ?? "0");

            if (CurrentUser.Id != userId)
                return Unauthorized();

            _userRepository.UpdateUserForEditProfile(CurrentUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, CurrentUser.Email),
                new Claim("UserId", CurrentUser.Id.ToString()),
                new Claim("UserName", CurrentUser.Name),
                new Claim("UserSurname", CurrentUser.Surname),
                new Claim("UserSex", CurrentUser.Sex),
                new Claim("UserNumberPhone", CurrentUser.NumberPhone),
                new Claim("UserDateOfBirth", CurrentUser.DateOfBirth?.ToString("yyyy-MM-dd") ?? "")
            };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

            return RedirectToPage("Profile");
        }
    }
}
