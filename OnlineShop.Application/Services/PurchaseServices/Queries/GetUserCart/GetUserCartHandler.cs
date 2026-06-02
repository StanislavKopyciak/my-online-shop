using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.Product;

namespace OnlineShop.Application.Services.PurchaseServices.Queries.GetUserCart
{
    public class GetUserCartHandler : IRequestHandler<GetUserCartQuery, List<GetProductDTO>>
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMapper _mapper;

        public GetUserCartHandler(IPurchaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetProductDTO>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetPurchasedProductsAsync(request.UserId, cancellationToken);

            return _mapper.Map<List<GetProductDTO>>(products);
        }
    }
}