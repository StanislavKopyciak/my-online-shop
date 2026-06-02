using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;
using OnlineShop.Infrastructure.Data;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly OnlineShopContext _context;

    public PurchaseRepository(OnlineShopContext context)
    {
        _context = context;
    }

    public async Task AddPurchaseAsync(Purchase purchase, CancellationToken cancellationToken)
    {
        await _context.Purchases.AddAsync(
            purchase,
            cancellationToken);
    }



    public async Task<List<Product>> GetPurchasedProductsAsync(Guid buyerId, CancellationToken cancellationToken)
    {
        return await _context.Purchases
            .Where(x => x.BuyerId == buyerId)
            .Include(x => x.Product)
                .ThenInclude(p => p.Address)
            .Select(x => x.Product)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}