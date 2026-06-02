using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.DTOs.Product
{
    public class UpdateProductDTO 
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DeliveryEnum? Delivery { get; set; }
        public string? Image_Url { get; set; }
        public CategoryEnum? Category { get; set; }
        public AddressDTO? Address { get; set; }
    }
}
