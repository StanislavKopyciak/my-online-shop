using FluentValidation;
using OnlineShop.Application.Services.UserServices.Commands.EditProfile;

namespace OnlineShop.Application.Services.UserServices.Commands.EditProfile
{
    public class EditProfileValidator : AbstractValidator<EditProfileCommand>
    {
        public EditProfileValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage("'Password' must be at least 6 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("'Email' is not a valid email address.");

            RuleFor(x => x.NumberPhone)
                .NotEmpty()
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("'Number Phone' is not a valid phone number.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now)
                .WithMessage("'Date Of Birth' must be in the past.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'Name' must be 50 characters or fewer.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'Surname' must be 50 characters or fewer.");

            RuleFor(x => x.Sex)
                .IsInEnum()
                .WithMessage("'Sex' has an invalid value.");

            RuleFor(x => x.Patronymic)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("'Patronymic' must be 50 characters or fewer.");

            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("'Address' must not be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Address.Country)
                        .NotEmpty()
                        .MaximumLength(50)
                        .WithMessage("'Country' must be 50 characters or fewer.");

                    RuleFor(x => x.Address.City)
                        .NotEmpty()
                        .MaximumLength(50)
                        .WithMessage("'City' must be 50 characters or fewer.");

                    RuleFor(x => x.Address.Street)
                        .NotEmpty()
                        .MaximumLength(100)
                        .WithMessage("'Street' must be 100 characters or fewer.");

                    RuleFor(x => x.Address.House)
                        .NotEmpty()
                        .MaximumLength(20)
                        .WithMessage("'House' must be 20 characters or fewer.");

                    RuleFor(x => x.Address.Apartment)
                        .MaximumLength(20)
                        .WithMessage("'Apartment' must be 20 characters or fewer.");
                });
        }
    }
}
