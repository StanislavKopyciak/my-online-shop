using FluentValidation;
using OnlineShop.Application.DTOs.Address;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.ProductServices.Commands.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'Title' must be 50 characters or fewer.");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("'Description' must be 1000 characters or fewer.");

            RuleFor(x => x.Category)
                .IsInEnum()
                .WithMessage("'Category' is invalid.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("'Price' must be greater than 0.");

            RuleFor(x => x.Delivery)
                .IsInEnum()
                .WithMessage("'Delivery' is invalid.");

            RuleFor(x => x.Image_Url)
                .NotEmpty()
                .WithMessage("'Image URL' is required.");

            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("'Address' is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Address.Country)
                        .MaximumLength(50)
                        .WithMessage("'Country' must be 50 characters or fewer.");

                    RuleFor(x => x.Address.City)
                        .NotEmpty()
                        .MaximumLength(50)
                        .WithMessage("'City' is required and must be 50 characters or fewer.");

                    RuleFor(x => x.Address.Street)
                        .NotEmpty()
                        .MaximumLength(100)
                        .WithMessage("'Street' is required and must be 100 characters or fewer.");

                    RuleFor(x => x.Address.House)
                        .NotEmpty()
                        .MaximumLength(20)
                        .WithMessage("'House' is required and must be 20 characters or fewer.");

                    RuleFor(x => x.Address.Apartment)
                        .MaximumLength(20)
                        .WithMessage("'Apartment' must be 20 characters or fewer.");
                });
        }
    }
}
