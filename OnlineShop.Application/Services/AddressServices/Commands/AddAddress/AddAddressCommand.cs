

using MediatR;

namespace OnlineShop.Application.Services.AddressServices.Commands.AddAddress
{
    public class AddAddressCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
    }
}
