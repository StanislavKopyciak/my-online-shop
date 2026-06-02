using MediatR;
using OnlineShop.Application.Interfaces.Product;
using OnlineShop.Application.Services.ProductServices.Commands.BuyProduct;

public class BuyProductHandler
    : IRequestHandler<BuyProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public BuyProductHandler(IProductRepository productRepository, IPurchaseRepository purchaseRepository)
    {
        _productRepository = productRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<bool> Handle(BuyProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
            return false;

        if (product.Seller_Id == request.UserId)
            return false;

        if (product.IsSold)
            return false;

        product.IsSold = true;

        var purchase = new Purchase
        {
            BuyerId = request.UserId,
            ProductId = product.Id
        };

        await _purchaseRepository.AddPurchaseAsync(purchase, cancellationToken);

        await _purchaseRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}