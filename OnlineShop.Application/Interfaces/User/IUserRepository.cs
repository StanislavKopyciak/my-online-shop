namespace OnlineShop.Application.Interfaces.User
{
    public interface IUserRepository
    {
        Task<Core.Entities.User> GetUserByIdAsync (Guid id, CancellationToken cancellationToken);

        Task<Core.Entities.User> GetUserByEmailAsync (string email, CancellationToken cancellationToken);

        Task PostUserAsync(Core.Entities.User user, CancellationToken cancellationToken);

        Task SaveChangeAsync(CancellationToken cancellationToken);
    }
}
