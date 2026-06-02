using FluentValidation;

namespace OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail
{
    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("'Email' is not a valid email address.");
        }
    }
}
