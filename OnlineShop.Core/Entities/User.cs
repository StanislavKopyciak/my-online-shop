using OnlineShop.Core.Enums;

namespace OnlineShop.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public SexEnum? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Guid AddressID { get; set; }
        public Address Address { get; set; } = null!;

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
