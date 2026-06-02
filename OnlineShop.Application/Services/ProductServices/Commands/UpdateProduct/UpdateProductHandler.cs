using MediatR;
using OnlineShop.Application.Interfaces.Product;

namespace OnlineShop.Application.Services.ProductServices.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);

            if (product == null)
            {
                throw new Exception("Продукт не знайдений");
            }


            product.Title = request.Title ?? product.Title;
            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;
            product.Delivery = request.Delivery ?? product.Delivery;
            product.Image_Url = request.Image_Url ?? product.Image_Url;
            product.Category = request.Category ?? product.Category;

            if (request.Address != null)
            {
                product.Address.City = request.Address.City ?? product.Address.City;
                product.Address.Street = request.Address.Street ?? product.Address.Street;
                product.Address.Apartment = request.Address.Apartment ?? product.Address.Apartment;
                product.Address.Country = request.Address.Country ?? product.Address.Country;
                product.Address.House = request.Address.House ?? product.Address.House;
            }

            await _productRepository.SaveChangeAsync(cancellationToken);
            return product.Id;
        }
    }
}
