using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.DTOs.User
{
    public class UpdateUserDTO
    {
        public string? Name { get; set; } 
        public string? Surname { get; set; } 
        public string? Patronymic { get; set; } 
        public string? NumberPhone { get; set; } 
        public SexEnum? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public AddressDTO? Address { get; set; }
    }
}
