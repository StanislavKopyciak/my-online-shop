using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Interfaces.Product;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, List<GetProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<GetProductDTO>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(request.Category, cancellationToken);

            if (products == null || !products.Any())
            {
                return new List<GetProductDTO>();
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        }
    }
}
