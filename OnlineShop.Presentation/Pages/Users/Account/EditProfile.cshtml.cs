using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.User;
using OnlineShop.Application.Services.UserServices.Commands.EditProfile;
using OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail;
using System.Security.Claims;

namespace OnlineShop.Presentation.Pages.Users.Account
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditProfileModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public UpdateUserDTO Input { get; set; } = new();

        public async Task OnGetAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return;

            var user = await _mediator.Send(new GetUserByEmailQuery
            {
                Email = email
            });

            if (user == null)
                return;

            Input = _mapper.Map<UpdateUserDTO>(user);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var command = _mapper.Map<EditProfileCommand>(Input);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var id))
                return Unauthorized();

            command.Id = id;

            var result = await _mediator.Send(command);

            if (result == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Не вдалося оновити профіль");
                return Page();
            }

            return RedirectToPage("/Users/Account/Profile");
        }
    }
}