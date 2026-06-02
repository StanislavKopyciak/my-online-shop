using FluentValidation;

namespace OnlineShop.Application.Services.AddressServices.Commands.UpdateAddress
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'Country' is required and must be 50 characters or fewer.");

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'City' is required and must be 50 characters or fewer.");

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("'Street' is required and must be 100 characters or fewer.");

            RuleFor(x => x.House)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("'House' is required and must be 20 characters or fewer.");

            RuleFor(x => x.Apartment)
                .MaximumLength(20)
                .WithMessage("'Apartment' must be 20 characters or fewer.");
        }
    }
}
