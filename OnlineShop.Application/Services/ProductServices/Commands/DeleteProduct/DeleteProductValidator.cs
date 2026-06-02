using FluentValidation;

namespace OnlineShop.Application.Services.ProductServices.Commands.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>   
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();
            RuleFor(x => x.SellerId)
                .NotEmpty();
        }
    }
}
