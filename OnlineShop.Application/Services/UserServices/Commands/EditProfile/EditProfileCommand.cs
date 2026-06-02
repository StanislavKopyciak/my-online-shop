using MediatR;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.UserServices.Commands.EditProfile
{
    public class EditProfileCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? NumberPhone { get; set; }
        public SexEnum? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public AddressDTO Address { get; set; } = null!;
    }
}