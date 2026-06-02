using FluentValidation;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryValidator : AbstractValidator<GetProductsByCategoryQuery>
    {
        public GetProductsByCategoryValidator()
        {
            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("'Category' has an invalid value.");
        }
    }
}
