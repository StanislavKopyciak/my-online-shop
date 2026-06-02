using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.User;
using OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail;

namespace OnlineShop.Presentation.Pages.Users.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IMediator _mediator;

        public ProfileModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;

        }

        [BindProperty]
        public GetUserDTO CurrentUser { get; set; } = new();

        public async Task OnGetAsync()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return;

            var query = new GetUserByEmailQuery
            {
                Email = email
            };

            var user = await _mediator.Send(query);

            if (user != null)
            {
                CurrentUser = user;
            }
        }

        public IActionResult OnPostEdit()
        {
            return RedirectToPage("/Users/Account/EditProfile");
        }
    }
}



