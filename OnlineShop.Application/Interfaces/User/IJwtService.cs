namespace OnlineShop.Application.Interfaces.User
{
    public interface IJwtService
    {
        public string GenerateToken(Guid userId, string email);
    }
}
