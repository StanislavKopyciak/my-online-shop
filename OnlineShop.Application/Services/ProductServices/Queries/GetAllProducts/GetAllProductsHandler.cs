using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Interfaces.Product;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<GetProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAllProductsAsync(cancellationToken);
            return _mapper.Map<List<GetProductDTO>>(product);
        }
    }
}