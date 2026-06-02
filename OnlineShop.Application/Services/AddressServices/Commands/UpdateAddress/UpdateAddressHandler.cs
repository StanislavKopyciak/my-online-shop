using MediatR;
using OnlineShop.Application.Interfaces.Address;

namespace OnlineShop.Application.Services.AddressServices.Commands.UpdateAddress
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, Guid>
    {
        private readonly IAddressRepository _addressRepository;
        public UpdateAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<Guid> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAddressByIdAsync(request.Id, cancellationToken);

            if (address == null)
            {
                throw new Exception("Адрес не найден");
            }

            address.Country = request.Country;
            address.City = request.City;
            address.Street = request.Street;
            address.House = request.House;
            address.Apartment = request.Apartment;


            await _addressRepository.SaveChangeAsync(cancellationToken);
            return address.Id;
        }
    }
}
