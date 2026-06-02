using MediatR;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.ProductServices.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DeliveryEnum? Delivery { get; set; }
        public string? Image_Url { get; set; }
        public CategoryEnum? Category { get; set; }
        public AddressDTO Address { get; set; } = null!;
    }
}
