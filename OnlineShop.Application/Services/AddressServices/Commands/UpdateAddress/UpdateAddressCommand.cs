using MediatR;

namespace OnlineShop.Application.Services.AddressServices.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string House { get; set; } = string.Empty;
        public string? Apartment { get; set; }
    }
}
