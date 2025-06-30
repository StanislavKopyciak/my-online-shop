namespace The_cheapest_prices.Pages
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public byte With_Delivery { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int Seller_Id { get; set; }
        public DateTime Created_At { get; set; }
        public string? Image_Url { get; set; }
        public string? Category { get; set; }
        public byte Available { get; set; }
    }
}
