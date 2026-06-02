using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Commands.BuyProduct;
using OnlineShop.Application.Services.ProductServices.Commands.DeleteProduct;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductById;
using System.Security.Claims;

namespace OnlineShop.Presentation.Pages.Products
{
    [Authorize]
    public class ProductDetailModel : PageModel
    {
        private readonly IMediator _mediator;

        public ProductDetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public GetProductDTO Product { get; set; } = new GetProductDTO();
        public bool IsOwner { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var query = new GetProductByIdQuery
            {
                Id = id
            };

            Product = await _mediator.Send(query);

            if (Product == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                IsOwner = Product.Seller_Id == userId;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBuyAsync(Guid productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var command = new BuyProductCommand
            {
                ProductId = productId,
                UserId = userId
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Помилка покупки");
                return Page();
            }

            return RedirectToPage("/Users/Cart/Cart");
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var command = new DeleteProductCommand
            {
                ProductId = productId,
                SellerId = userId
            };

            var result = await _mediator.Send(command);

            if (result == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Не вдалося видалити товар");
                return Page();
            }

            return RedirectToPage("/Products/Index");
        }
    }
}