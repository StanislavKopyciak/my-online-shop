using FluentValidation;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductById
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
