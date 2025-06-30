using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using The_cheapest_prices.Pages.Services;

namespace The_cheapest_prices.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly HashService _hashService;
        public RegistrationModel(AuthService authService, HashService hashService)
        {
            _authService = authService;
            _hashService = hashService;
        }

        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Surname { get; set; }
        [BindProperty]
        public string? NumberPhone { get; set; }
        [BindProperty]
        public string? Sex { get; set; }
        [BindProperty]
        public DateOnly? DateOfBirth { get; set; }
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Password { get; set; }
        public string? Message { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "¬вед≥ть пароль");
                return Page();
            }

            string? hashedPassword = _hashService.HashPassword(Password);
            var user = new User
            {
                Name = Name,
                Surname = Surname,
                NumberPhone = NumberPhone,
                Sex = Sex,
                DateOfBirth = DateOfBirth,
                Email = Email,
                Password = hashedPassword
            };

            Message = _authService.Registrtion(user);

            if (Message == null)
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
