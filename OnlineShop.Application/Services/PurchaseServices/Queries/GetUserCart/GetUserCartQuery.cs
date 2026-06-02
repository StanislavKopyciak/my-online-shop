using MediatR;
using OnlineShop.Application.DTOs.Product;

namespace OnlineShop.Application.Services.PurchaseServices.Queries.GetUserCart
{
    public class GetUserCartQuery : IRequest<List<GetProductDTO>>
    {
        public Guid UserId { get; set; }
    }
}