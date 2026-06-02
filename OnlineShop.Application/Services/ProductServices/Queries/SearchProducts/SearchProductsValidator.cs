using FluentValidation;

namespace OnlineShop.Application.Services.ProductServices.Queries.SearchProducts
{
    public class SearchProductsValidator : AbstractValidator<SearchProductsQuery>
    {
        public SearchProductsValidator()
        {
            RuleFor(x => x.SearchText)
                .MaximumLength(100)
                .WithMessage("'Search Text' must be 100 characters or fewer.");
        }
    }
}
