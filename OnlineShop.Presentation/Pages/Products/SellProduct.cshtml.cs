using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Commands.CreateProduct;
using System.Security.Claims;

namespace OnlineShop.Pages.Products
{
    [Authorize]
    public class SellProductModel : PageModel
    {
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SellProductModel(IMediator mediator, IMapper mapper, IValidator<CreateProductCommand> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
        }

        [BindProperty]
        public CreateProductDTO Input { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var sellerId))
                return Unauthorized();

            var command = _mapper.Map<CreateProductCommand>(Input);

            command.Seller_Id = sellerId;

            string imagePath = "";

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            command.Image_Url = imagePath;

            var validation = await _validator.ValidateAsync(command);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);

                return Page();
            }

            var result = await _mediator.Send(command);

            if (result == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Не вдалося створити товар");
                return Page();
            }

            return RedirectToPage("/Products/Index");
        }
    }
}