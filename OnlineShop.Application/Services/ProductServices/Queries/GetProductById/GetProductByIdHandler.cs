using MediatR;
using OnlineShop.Application.DTOs.Product;
using AutoMapper;
using OnlineShop.Core.Entities;
using OnlineShop.Application.Interfaces.Product;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductDTO>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<GetProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new Exception("Продукт не знайдений");
            }
            return _mapper.Map<GetProductDTO>(product);
        }
    }
}
