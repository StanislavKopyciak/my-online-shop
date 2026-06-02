using MediatR;

namespace OnlineShop.Application.Services.ProductServices.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
    }
}
