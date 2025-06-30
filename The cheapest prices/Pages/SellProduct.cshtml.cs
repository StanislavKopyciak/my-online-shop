using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using The_cheapest_prices.Pages;
using The_cheapest_prices.Pages.Services;

namespace The_cheapest_prices.Pages
{ 
    public class SellProductModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _environment;

        public SellProductModel(ProductService productService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _environment = environment;
        }

        [BindProperty]
        public string? Title { get; set; }
        [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        public decimal Price { get; set; }
        [BindProperty]
        public byte With_Delivery { get; set; }
        [BindProperty]
        public string? City { get; set; }
        [BindProperty]
        public string? Address { get; set; }
        [BindProperty]
        public string? Category { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var claims = User.Claims.ToList();
            int UserId = int.TryParse(claims.FirstOrDefault(c => c.Type == "UserId")?.Value, out var id) ? id : 0;

            var product = new Product
            {
                Title = Title,
                Description = Description,
                Price = Price,
                With_Delivery = With_Delivery,
                City = City,
                Address = Address,
                Category = Category,
                Seller_Id = UserId
            };

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.Image_Url = "uploads/" + fileName;  
            }
            else
            {
                product.Image_Url = null;
            }

            var validationMessage = _productService.ValidateProduct(product);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                Message = validationMessage;
                return Page();
            }

            _productService.SaveProduct(product);

            return RedirectToPage("/Index");
        }
    }
}