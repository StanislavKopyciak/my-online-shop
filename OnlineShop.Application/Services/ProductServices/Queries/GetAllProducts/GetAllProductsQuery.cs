using MediatR;
using OnlineShop.Application.DTOs.Product;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<GetProductDTO>>
    {
 
    }
}
