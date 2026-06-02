using MediatR;
using OnlineShop.Application.DTOs.User;

namespace OnlineShop.Application.Services.UserServices.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserDTO>
    {
        public Guid Id { get; set; }
    }
}

