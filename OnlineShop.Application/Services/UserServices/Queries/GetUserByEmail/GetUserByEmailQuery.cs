using MediatR;
using OnlineShop.Application.DTOs.User;

namespace OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<GetUserDTO>
    {
        public string Email { get; set; } = string.Empty;
    }
}
