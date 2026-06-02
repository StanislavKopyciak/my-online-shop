using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductsBySellerId;
using System.Security.Claims;

namespace OnlineShop.Presentation.Pages.Products
{
    [Authorize]
    public class MyProductsModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IValidator<GetProductsBySellerIdQuery> _validator;
        private readonly IMapper _mapper;

        public MyProductsModel(IMediator mediator, IValidator<GetProductsBySellerIdQuery> validator, IMapper mapper)
        {
            _mediator = mediator;
            _validator = validator;
            _mapper = mapper;
        }

        public List<GetProductDTO> MyProducts { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return;

            var query = new GetProductsBySellerIdQuery
            {
                SellerId = userId
            };

            var validate = await _validator.ValidateAsync(query);

            if (!validate.IsValid)
            {
                throw new Exception("Помилка валідації");
            }

            var results = await _mediator.Send(query);

            if (results == null)
            {
                throw new Exception("Помилка отримання товарів");
            }

            MyProducts = results;
        }
    }
}