using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Product;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Enums;

namespace OnlineShop.Infrastructure.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopContext _context;

        public ProductRepository(OnlineShopContext context) {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(x => x.Address)
                .Where(x => !x.IsSold)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(u => u.Address).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return product;
        }

        public async Task PostProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Include(u => u.Address).Where(x => x.Seller_Id == sellerId).ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(CategoryEnum category, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(x => x.Address)
                .Where(x => x.Category == category && !x.IsSold)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> SearchAsync(string? searchText, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Product>();

            searchText = searchText.Trim();

            return await _context.Products
                .Where(x => !x.IsSold)
                .Where(x => EF.Functions.ILike(x.Title, $"%{searchText}%"))
                .ToListAsync(cancellationToken);
        }
    }
}
