using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Queries.SearchProducts;

namespace OnlineShop.Presentation.Pages.Products
{
    [AllowAnonymous]
    public class SearchModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IValidator<SearchProductsQuery> _validator;

        public SearchModel(IMediator mediator, IValidator<SearchProductsQuery> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchText { get; set; }

        public List<GetProductDTO> Results { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            var query = new SearchProductsQuery
            {
                SearchText = SearchText
            };

            var validation = await _validator.ValidateAsync(query);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return Page();
            }

            Results = await _mediator.Send(query);

            return Page();
        }
    }
}