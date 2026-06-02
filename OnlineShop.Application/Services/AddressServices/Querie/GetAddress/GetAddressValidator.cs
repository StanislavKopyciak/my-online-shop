
using FluentValidation;

namespace OnlineShop.Application.Services.AddressServices.Querie.GetAddress
{
    public class GetAddressValidator : AbstractValidator<GetAddressQuery>
    {
        public GetAddressValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
