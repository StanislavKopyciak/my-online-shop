using OnlineShop.Core.Entities;

public interface IPurchaseRepository
{
    Task AddPurchaseAsync(Purchase purchase, CancellationToken cancellationToken);

    Task<List<Product>> GetPurchasedProductsAsync(
        Guid buyerId,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}