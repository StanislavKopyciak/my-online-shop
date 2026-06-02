using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.User;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineShopContext _context;

        public UserRepository(OnlineShopContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
        }

        public async Task PostUserAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
