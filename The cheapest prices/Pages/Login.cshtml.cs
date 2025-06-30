using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Cryptography;
using The_cheapest_prices.Pages.Data;
using The_cheapest_prices.Pages.Services;

namespace The_cheapest_prices.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly UserRepository _userRepository;
        private readonly HashService _hashService;
        public LoginModel(AuthService authService, UserRepository userRepository, HashService hashService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _hashService = hashService;
        }

        DataTable table = new DataTable();

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Password { get; set; } 

        public string? Message { get; set; }
        
        
        public async Task<IActionResult> OnPostAsync()
        {

            string? hashedPassword = _hashService.HashPassword(Password);
            User tempUser = new User
            {
                Email = Email,
                Password = hashedPassword
            };

            Message = await _authService.AuthenticateAsync(tempUser, table);

            if (Message == "Успішна авторизація!")
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, Message);
                return Page();
            }
        }
    }
}
