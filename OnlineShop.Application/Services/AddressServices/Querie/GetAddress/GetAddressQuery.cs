using MediatR;
using OnlineShop.Core.Entities;

namespace OnlineShop.Application.Services.AddressServices.Querie.GetAddress
{
    public class GetAddressQuery : IRequest<Address>
    {
        public Guid Id { get; set; }
    }
}
