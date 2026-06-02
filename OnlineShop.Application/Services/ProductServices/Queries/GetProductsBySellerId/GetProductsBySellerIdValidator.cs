using FluentValidation;

namespace OnlineShop.Application.Services.ProductServices.Queries.GetProductsBySellerId
{
    public class GetProductsBySellerIdValidator : AbstractValidator<GetProductsBySellerIdQuery>
    {
        public GetProductsBySellerIdValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty();
        }
    }
}
