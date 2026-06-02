namespace OnlineShop.Application.Interfaces.Address
{
    public interface IAddressRepository
    {
        Task AddAddressAsync(Core.Entities.Address address, CancellationToken cancellationToken);
        Task<Core.Entities.Address> GetAddressByIdAsync(Guid id, CancellationToken cancellationToken);
        Task SaveChangeAsync(CancellationToken cancellationToken);
    }
}
