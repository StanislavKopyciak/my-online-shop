using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.DTOs.Product
{
    public class GetProductDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DeliveryEnum Delivery { get; set; }
        public Guid Seller_Id { get; set; }
        public DateTime Created_At { get; set; } 
        public string Image_Url { get; set; } = string.Empty;
        public CategoryEnum Category { get; set; }

        public bool IsSold { get; set; } = false;

        public AddressDTO? Address { get; set; }
    }
}
