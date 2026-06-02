using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.PurchaseServices.Queries.GetUserCart;
using System.Security.Claims;

namespace OnlineShop.Pages.Users.Cart
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly IMediator _mediator;

        public CartModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<GetProductDTO> Cart { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var id))
            {
                Cart = new();
                return;
            }

            Cart = await _mediator.Send(new GetUserCartQuery
            {
                UserId = id
            });
        }
    }
}