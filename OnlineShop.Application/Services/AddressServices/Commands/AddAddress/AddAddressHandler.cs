using AutoMapper;
using MediatR;
using OnlineShop.Application.Interfaces.Address;
using OnlineShop.Core.Entities;

namespace OnlineShop.Application.Services.AddressServices.Commands.AddAddress
{
    public class AddAddressHandler : IRequestHandler<AddAddressCommand, Guid>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public AddAddressHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request);

            await _addressRepository.AddAddressAsync(address, cancellationToken);

            return address.Id;
        }
    }
}
