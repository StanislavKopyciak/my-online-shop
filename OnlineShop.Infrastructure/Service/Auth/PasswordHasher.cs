using OnlineShop.Application.Interfaces.User;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Application.Services.UserServices
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] b = Encoding.ASCII.GetBytes(password ?? "");
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();

            foreach (var a in hash)
            {
                sb.Append(a.ToString("X2"));
            }

            return sb.ToString();
        }

        public string VerifyPassword(string password, string hashedPassword)
        {
            string hashedInputPassword = HashPassword(password) ?? throw new Exception("Помилка з паролем");
            return hashedInputPassword == hashedPassword ? hashedPassword : string.Empty;
        }
    }
}
