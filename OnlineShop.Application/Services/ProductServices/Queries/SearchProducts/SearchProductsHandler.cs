using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Interfaces.Product;

namespace OnlineShop.Application.Services.ProductServices.Queries.SearchProducts
{
    public class SearchProductsHandler : IRequestHandler<SearchProductsQuery, List<GetProductDTO>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public SearchProductsHandler(
            IProductRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetProductDTO>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.SearchAsync(request.SearchText, cancellationToken);

            return _mapper.Map<List<GetProductDTO>>(products);
        }
    }
}