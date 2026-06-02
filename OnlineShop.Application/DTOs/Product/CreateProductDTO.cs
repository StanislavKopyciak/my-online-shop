using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.DTOs.Product
{
    public class CreateProductDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DeliveryEnum Delivery { get; set; }
        public string Image_Url { get; set; } = string.Empty;
        public CategoryEnum Category { get; set; }
        public AddressDTO? Address { get; set; }
        public Guid Seller_Id { get; set; }
    }
}
