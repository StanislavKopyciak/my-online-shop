using MediatR;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQuery : IRequest<List<GetProductDTO>>
    {
        public CategoryEnum Category { get; set; } 
    }
}
