using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Queries.GetAllProducts;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductsByCategory;
using OnlineShop.Core.Enums;

namespace OnlineShop.Pages.Products
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<GetProductDTO> Products { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public CategoryEnum? SelectedCategory { get; set; }

        public async Task OnGetAsync()
        {
            if (SelectedCategory == null)
            {
                Products = await _mediator.Send(new GetAllProductsQuery());
                return;
            }

            Products = await _mediator.Send(new GetProductsByCategoryQuery
            {
                Category = SelectedCategory.Value
            });
        }
    }
}