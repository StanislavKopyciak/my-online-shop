using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.User;
using OnlineShop.Application.Interfaces.User;

namespace OnlineShop.Application.Services.UserServices.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetUserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user == null)
                throw new Exception("Юзер не знайдений");


            var userDTO = _mapper.Map<GetUserDTO>(user);

            return userDTO;
        }
    }
}
