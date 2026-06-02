namespace OnlineShop.Application.Interfaces.User
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        string VerifyPassword(string password, string hashedPassword);
    }
}
