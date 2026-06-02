using MediatR;
using OnlineShop.Application.DTOs.Product;

namespace OnlineShop.Application.Services.ProductServices.Queries.SearchProducts
{
    public class SearchProductsQuery : IRequest<List<GetProductDTO>>
    {
        public string? SearchText { get; set; }
    }
}