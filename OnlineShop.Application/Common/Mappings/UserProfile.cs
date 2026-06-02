using AutoMapper;
using OnlineShop.Application.DTOs.User;
using OnlineShop.Application.Services.UserServices.Commands.EditProfile;
using OnlineShop.Application.Services.UserServices.Commands.Login;
using OnlineShop.Application.Services.UserServices.Commands.Register;
using OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail;
using OnlineShop.Application.Services.UserServices.Queries.GetUserById;
using OnlineShop.Core.Entities;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDTO, RegisterCommand>();
        CreateMap<LoginDTO, LoginCommand>();
        CreateMap<UpdateUserDTO, EditProfileCommand>();

        CreateMap<User, GetUserDTO>();
        CreateMap<User, GetUserByIdQuery>().ReverseMap();
        CreateMap<User, GetUserByEmailQuery>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
        CreateMap<GetUserDTO, UpdateUserDTO>().ReverseMap();
    }
}