namespace OnlineShop.Core.Entities
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
    }
}
