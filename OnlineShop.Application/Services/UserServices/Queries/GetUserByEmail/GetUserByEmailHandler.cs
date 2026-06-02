using AutoMapper;
using MediatR;
using OnlineShop.Application.DTOs.User;
using OnlineShop.Application.Interfaces.User;

namespace OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, GetUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByEmailHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                throw new Exception("Юзер не знайдений");
            }

            return _mapper.Map<GetUserDTO>(user);
        }
    }
}
