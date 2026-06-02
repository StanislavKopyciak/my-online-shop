using FluentValidation;


namespace OnlineShop.Application.Services.UserServices.Queries.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
