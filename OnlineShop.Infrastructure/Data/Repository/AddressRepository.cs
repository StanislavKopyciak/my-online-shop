using OnlineShop.Application.Interfaces.Address;

namespace OnlineShop.Infrastructure.Data.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly OnlineShopContext _context;

        public AddressRepository(OnlineShopContext context)
        {
            _context = context;
        }

        public async Task AddAddressAsync(Core.Entities.Address address, CancellationToken cancellationToken)
        {
            await _context.Address.AddAsync(address, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<Core.Entities.Address> GetAddressByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.Address.FindAsync([id], cancellationToken);
            return result!;
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
