using MediatR;
using OnlineShop.Application.DTOs.Product;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<GetProductDTO>
    {
        public Guid Id { get; set; }
    }
}
