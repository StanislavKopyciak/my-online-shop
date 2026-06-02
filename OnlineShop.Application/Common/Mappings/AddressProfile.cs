using AutoMapper;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Application.Services.AddressServices.Commands.AddAddress;
using OnlineShop.Application.Services.AddressServices.Commands.UpdateAddress;
using OnlineShop.Application.Services.AddressServices.Querie.GetAddress;
using OnlineShop.Core.Entities;

namespace OnlineShop.Application.Common.Mappings
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressDTO, Address>().ReverseMap(); 
            CreateMap<Address, AddressDTO>().ReverseMap(); 

            CreateMap<Address, AddAddressCommand>().ReverseMap();
            CreateMap<Address, UpdateAddressCommand>().ReverseMap();
            CreateMap<Address, GetAddressQuery>().ReverseMap();
        }
    }
}
