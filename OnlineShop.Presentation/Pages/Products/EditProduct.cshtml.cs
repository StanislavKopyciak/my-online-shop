using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Commands.UpdateProduct;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductById;
using System.Security.Claims;

namespace OnlineShop.Presentation.Pages.Products
{
    [Authorize]
    public class EditProductModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditProductModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public UpdateProductDTO Input { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery
            {
                Id = id
            });

            if (product == null)
                return NotFound();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            if (product.Seller_Id != userId)
                return Forbid();

            Input = _mapper.Map<UpdateProductDTO>(product);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var command = _mapper.Map<UpdateProductCommand>(Input);

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);

                var folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                command.Image_Url = "/images/" + fileName;
            }

            var result = await _mediator.Send(command);

            if (result == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Не вдалося оновити товар");
                return Page();
            }

            return RedirectToPage("/Products/ProductDetail", new { id = result });
        }
    }
}