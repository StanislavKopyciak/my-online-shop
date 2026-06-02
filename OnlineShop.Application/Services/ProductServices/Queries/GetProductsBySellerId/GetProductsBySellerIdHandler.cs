using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Interfaces.Product;
using System.Linq;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductsBySellerId
{
    public class GetProductsBySellerIdHandler : IRequestHandler<GetProductsBySellerIdQuery, List<GetProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductsBySellerIdHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<GetProductDTO>> Handle(GetProductsBySellerIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsBySellerIdAsync(request.SellerId, cancellationToken);

            return _mapper.Map<List<GetProductDTO>>(products);
        }
    }
}
