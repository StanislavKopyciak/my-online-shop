using MediatR;
using OnlineShop.Application.Interfaces.User;

namespace OnlineShop.Application.Services.UserServices.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginHandler(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtService jwtService)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null)
                throw new Exception("Користувач не знайдений");

            var hashedPassword = _passwordHasher.VerifyPassword(request.Password, user.Password);

            if (hashedPassword == string.Empty)
                throw new Exception("Невірний пароль");
            
            var jwt = _jwtService.GenerateToken(user.Id, user.Email);

            return jwt;
        }
    }
}
