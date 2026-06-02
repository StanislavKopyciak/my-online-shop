using MediatR;
using OnlineShop.Application.Interfaces.Product;
using OnlineShop.Core.Entities;

namespace OnlineShop.Application.Services.ProductServices.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Image_Url = request.Image_Url,
                Category = request.Category,
                Delivery = request.Delivery,
                Seller_Id = request.Seller_Id,
                Address = new Address
                {
                    Country = request.Address.Country,
                    City = request.Address.City,
                    Street = request.Address.Street,
                    House = request.Address.House,
                    Apartment = request.Address.Apartment
                }
            };
            await _productRepository.PostProductAsync(product, cancellationToken);
            await _productRepository.SaveChangeAsync(cancellationToken);

            return product.Id;

        }
    }
}
