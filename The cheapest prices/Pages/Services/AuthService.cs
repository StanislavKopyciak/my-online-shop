using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Data;
using System.Security.Claims;
using The_cheapest_prices.Pages.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace The_cheapest_prices.Pages.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AuthenticateAsync(User user, DataTable table)
        {
            var errors = ValidateLogin(user);
            if (errors.Count > 0)
                return string.Join("; ", errors);

            _userRepository.GetUserForLogin(user, table);

            foreach (DataRow row in table.Rows)
            {
                string dbEmail = row["email"]?.ToString()?.Trim() ?? "";
                string dbPassword = row["password"]?.ToString()?.Trim() ?? "";

                if ((dbEmail == (user.Email?.Trim() ?? "")) && (dbPassword == (user.Password?.Trim() ?? "")))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, dbEmail),
                        new Claim("UserId", row["id"]?.ToString() ?? ""),
                        new Claim("UserName", row["name"]?.ToString() ?? ""),
                        new Claim("UserSurname", row["surname"]?.ToString() ?? ""),
                        new Claim("UserSex", row["sex"]?.ToString() ?? ""),
                        new Claim("UserNumberPhone", row["numberphone"]?.ToString() ?? ""),
                        new Claim("UserDateOfBirth",
                            DateOnly.FromDateTime(Convert.ToDateTime(row["dateofbirth"])).ToString("yyyy-MM-dd"))
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var context = _httpContextAccessor.HttpContext;
                    if (context == null)
                        throw new InvalidOperationException("HttpContext is null. Авторизація неможлива.");

                    await context.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddHours(1)
                        });

                    return "Успішна авторизація!";
                }
            }

            return "Невірний email або пароль.";
        }



        public string? Registrtion(User user)
        {
            var errors = ValidateRegistration(user);
            if (errors.Count > 0)
            {
                return string.Join("; ", errors);
            }

            int result = _userRepository.PostUserForRegistration(user);

            if (result == 1)
            {
                return null; 
            }
            else
            {
                return "Помилка при створені";
            }
        }

        public List<string> ValidateLogin(User user)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(user.Email)) errors.Add("Email не заповнено.");
            if (string.IsNullOrWhiteSpace(user.Password)) errors.Add("Пароль не заповнено.");
            return errors;
        }

        public List<string> ValidateRegistration(User user)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(user.Name)) errors.Add("Ім'я не заповнено.");
            if (string.IsNullOrWhiteSpace(user.Surname)) errors.Add("Прізвище не заповнено.");
            if (string.IsNullOrWhiteSpace(user.Sex)) errors.Add("Стать не вибрана.");
            if (string.IsNullOrWhiteSpace(user.NumberPhone)) errors.Add("Номер телефону не заповнено.");
            if (string.IsNullOrWhiteSpace(user.Email)) errors.Add("Email не заповнено.");
            if (string.IsNullOrWhiteSpace(user.Password)) errors.Add("Пароль не заповнено.");
            if (user.NumberPhone?.Length != 10) errors.Add("Неправильно заповнений номер");
            if (user.Name?.Count(c => !char.IsWhiteSpace(c)) < 3) errors.Add("В імені повинно бути більше, чим три символи");
            if (user.Surname?.Count(c => !char.IsWhiteSpace(c)) < 3) errors.Add("В фамілії повинно бути більше, чим три символи");
            if (user.Name?.Count(c => !char.IsWhiteSpace(c)) > 20) errors.Add("В імені повинно бути менше 20 символів");
            if (user.Surname?.Count(c => !char.IsWhiteSpace(c)) > 20) errors.Add("В фамілії повинно бути менше 20 символів");
            

            return errors;
        }
    }
}


