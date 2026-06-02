using OnlineShop.Core.Enums;

namespace OnlineShop.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DeliveryEnum Delivery { get; set; }
        public Guid Seller_Id { get; set; } 
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public string Image_Url { get; set; } = string.Empty;
        public CategoryEnum Category { get; set; }

        public Guid AddressID { get; set; }
        public Address Address { get; set; } = null!;

        public bool IsSold { get; set; } = false;

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
