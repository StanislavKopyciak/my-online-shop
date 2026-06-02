using MediatR;
using OnlineShop.Application.Interfaces.User;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Enums;

namespace OnlineShop.Application.Services.UserServices.Commands.EditProfile
{
    public class EditProfileHandler : IRequestHandler<EditProfileCommand, Guid>
    {
        private readonly IUserRepository _userRepository;   

        public EditProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user == null)
                throw new Exception("Юзер не знайдений");

            user.Name = request.Name ?? user.Name;
            user.Surname = request.Surname ?? user.Surname;
            user.Patronymic = request.Patronymic ?? user.Patronymic;
            user.NumberPhone = request.NumberPhone ?? user.NumberPhone;
            user.Sex = request.Sex ?? user.Sex;
            user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
            user.Email = request.Email ?? user.Email;
            user.Password = request.Password ?? user.Password;

            if (user.Address != null && request.Address != null)
            {
                user.Address.Country = request.Address.Country ?? user.Address.Country;
                user.Address.City = request.Address.City ?? user.Address.City;
                user.Address.Street = request.Address.Street ?? user.Address.Street;
                user.Address.House = request.Address.House ?? user.Address.House;
                user.Address.Apartment = request.Address.Apartment ?? user.Address.Apartment;
            }
            else if (request.Address != null)
            {
                user.Address = new Address
                {
                    Country = request.Address.Country,
                    City = request.Address.City,
                    Street = request.Address.Street,
                    House = request.Address.House,
                    Apartment = request.Address.Apartment
                };
            }

            await _userRepository.SaveChangeAsync(cancellationToken);
            return request.Id;
        }
    }
}
