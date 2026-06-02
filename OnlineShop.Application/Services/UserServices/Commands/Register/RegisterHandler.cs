using AutoMapper;
using MediatR;
using OnlineShop.Application.Interfaces.User;
using OnlineShop.Core.Entities;

namespace OnlineShop.Application.Services.UserServices.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user != null)
                throw new Exception("Пользователь с таким email уже существует.");

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var newUser = _mapper.Map<User>(request);

            newUser.Password = hashedPassword;

            await _userRepository.PostUserAsync(newUser, cancellationToken);

            var jwt = _jwtService.GenerateToken(newUser.Id, newUser.Email);

            return jwt;
        }
    }
}
