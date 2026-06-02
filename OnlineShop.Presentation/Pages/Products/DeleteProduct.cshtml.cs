using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.Services.ProductServices.Commands.DeleteProduct;
using System.Security.Claims;

namespace OnlineShop.Presentation.Pages.Products
{
    [Authorize]
    public class DeleteProductModel : PageModel
    {
        private readonly IValidator<DeleteProductCommand> _validator;
        private readonly IMediator _mediator;

        public DeleteProductModel(
            IValidator<DeleteProductCommand> validator,
            IMediator mediator)
        {
            _validator = validator;
            _mediator = mediator;
        }

        public async Task<IActionResult> OnPostAsync(Guid productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var command = new DeleteProductCommand
            {
                ProductId = productId,
                SellerId = userId
            };

            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return Page();
            }

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