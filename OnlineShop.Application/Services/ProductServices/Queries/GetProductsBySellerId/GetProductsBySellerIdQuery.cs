using MediatR;
using OnlineShop.Application.DTOs.Product;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductsBySellerId
{
    public class GetProductsBySellerIdQuery : IRequest<List<GetProductDTO>>
    {
        public Guid SellerId { get; set; }
    }
}
