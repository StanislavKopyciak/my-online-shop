using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Services.UserServices.Commands.Register;
using AutoMapper;
using OnlineShop.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Presentation.Pages.Users.Auth
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IValidator<RegisterCommand> _validator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterModel(IValidator<RegisterCommand> validator, IMediator mediator, IMapper mapper)
        {
            _validator = validator;
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public RegisterDTO Input { get; set; } = new RegisterDTO();

        public async Task<IActionResult> OnPostAsync()
        {
            var command = _mapper.Map<RegisterCommand>(Input);

            var validate = await _validator.ValidateAsync(command);

            if (!validate.IsValid)
            {
                foreach (var error in validate.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
                return Page();
            }

            var result = await _mediator.Send(command);

            if (result != string.Empty)
            {
                Response.Cookies.Append(
                                "jwt",
                                result,
                                new CookieOptions
                                {
                                    HttpOnly = true,            
                                    Secure = true,             
                                    SameSite = SameSiteMode.Strict,
                                    Path = "/",
                                    Expires = DateTimeOffset.Now.AddDays(1)
                                });

                return RedirectToPage("/Products/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Помилка реєстрації. Спробуйте ще раз.");
                return Page();
            }
        }
    }
}
