using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.DTOs.User
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public SexEnum? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;

        public AddressDTO? Address { get; set; }
    }
}
