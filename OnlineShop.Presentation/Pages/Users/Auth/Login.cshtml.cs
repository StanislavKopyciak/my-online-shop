using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.User;
using OnlineShop.Application.Services.UserServices.Commands.Login;
 

namespace OnlineShop.Presentation.Pages.Users.Auth
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IValidator<LoginCommand> _validator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoginModel(IValidator<LoginCommand> validator, IMediator mediator, IMapper mapper)
        {
            _validator = validator;
            _mediator = mediator;
            _mapper = mapper;
        }


        [BindProperty]
        public LoginDTO Input { get; set; } = new LoginDTO();

        public async Task<IActionResult> OnPostAsync()
        {

            var command = _mapper.Map<LoginCommand>(Input);

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
                                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                                });

                return RedirectToPage("/Products/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Невірний email або пароль.");
                return Page();
            }
        }
    }
}
