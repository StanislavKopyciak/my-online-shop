namespace OnlineShop.Application.Interfaces.Product
{
    public interface IProductRepository
    {
            Task<Core.Entities.Product> GetProductByIdAsync (Guid id, CancellationToken cancellationToken);
    
            Task<List<Core.Entities.Product>> GetAllProductsAsync(CancellationToken cancellationToken);
    
            Task PostProductAsync(Core.Entities.Product product, CancellationToken cancellationToken);
    
            Task SaveChangeAsync(CancellationToken cancellationToken);
    
            Task DeleteProductAsync(Core.Entities.Product product, CancellationToken cancellationToken);

            Task<List<Core.Entities.Product>> GetProductsBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken);
            
            Task<List<Core.Entities.Product>> GetProductsByCategoryAsync(Core.Enums.CategoryEnum category, CancellationToken cancellationToken);

            Task<List<Core.Entities.Product>> SearchAsync(string? searchText, CancellationToken cancellationToken);
    }
}
