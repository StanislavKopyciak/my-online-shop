
using MediatR;
using OnlineShop.Application.Interfaces.Address;
using OnlineShop.Core.Entities;

namespace OnlineShop.Application.Services.AddressServices.Querie.GetAddress
{
    public class GetAddressHandler : IRequestHandler<GetAddressQuery, Address>
    {
        private readonly IAddressRepository _addressRepository;

        public GetAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAddressByIdAsync(request.Id, cancellationToken);
            if (address == null)
            {
                throw new Exception("Адреса не знайдена");
            }
            return address;
        }
    }
}
