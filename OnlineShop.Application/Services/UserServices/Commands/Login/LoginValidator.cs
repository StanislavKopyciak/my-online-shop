using FluentValidation;

namespace OnlineShop.Application.Services.UserServices.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("'Email' is not a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("'Password' must be at least 6 characters long.");
        }
    }
}
