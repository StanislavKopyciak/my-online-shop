using OnlineShop.Core.Entities;

public class Purchase
{
    public Guid Id { get; set; }

    public Guid BuyerId { get; set; }
    public User Buyer { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
}