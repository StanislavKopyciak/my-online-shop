using MediatR;
using OnlineShop.Application.Interfaces.Product;

namespace OnlineShop.Application.Services.ProductServices.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new Exception("Продукт не знайдений");
            }

            if (product.Seller_Id != request.SellerId)
            {
                throw new Exception("Ви не маєте доступ до цього товару");
            }

            if (product.IsSold == true)
            {
                throw new Exception("Неможливо видалити проданий товар");
            }


            await _productRepository.DeleteProductAsync(product, cancellationToken);

            return product.Id;
        }
    }
}
