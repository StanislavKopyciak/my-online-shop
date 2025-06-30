using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using The_cheapest_prices.Pages.Data;
using Microsoft.AspNetCore.Mvc;

namespace The_cheapest_prices.Pages
{
    //[Authorize] // Забороняє доступ неавторизованим
    public class ProfileModel : PageModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string UserSurname { get; set; } = "";
        public string UserSex { get; set; } = "";
        public DateOnly UserDateOfBirth { get; set; } = DateOnly.MinValue;
        public string UserNumberPhone { get; set; } = "";
        public string UserEmail { get; set; } = "";

        public void OnGet()
        {
            var claims = User.Claims.ToList();

            UserEmail = User.FindFirstValue(ClaimTypes.Email) ?? "";

            UserId = int.TryParse(claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var id) ? id : 0;
            UserName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value ?? "";
            UserSurname = claims.FirstOrDefault(c => c.Type == "UserSurname")?.Value ?? "";
            UserSex = claims.FirstOrDefault(c => c.Type == "UserSex")?.Value ?? "";
            UserNumberPhone = claims.FirstOrDefault(c => c.Type == "UserNumberPhone")?.Value ?? "";

            if (DateOnly.TryParse(claims.FirstOrDefault(c => c.Type == "UserDateOfBirth")?.Value, out var dob))
            {
                UserDateOfBirth = dob;
            }
        }

        
        public IActionResult OnPostEdit()
        {
            return RedirectToPage("EditProfile");
        }
    }
}




