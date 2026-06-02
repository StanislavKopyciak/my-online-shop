using MediatR;

namespace OnlineShop.Application.Services.ProductServices.Commands.BuyProduct
{
    public class BuyProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; } 
    }
}