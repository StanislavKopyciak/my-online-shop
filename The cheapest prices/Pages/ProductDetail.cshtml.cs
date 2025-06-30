using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using The_cheapest_prices.Pages.Data;
using The_cheapest_prices.Pages.Services;

namespace The_cheapest_prices.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductService _productService;

        public ProductDetailModel(ProductRepository productRepository, ProductService productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        public Product? Product { get; set; }

        public IActionResult OnGet(int id)
        {
            Product = _productRepository.GetProductById(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostBuy(int productId)
        {
            _productRepository.UpdateProductAvailability(productId, false);

            var productToAdd = _productRepository.GetProductById(productId);
            if (productToAdd == null)
            {
                return NotFound();
            }

            List<Product> cart;
            var cartJson = HttpContext.Session.GetString("cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                cart = new List<Product>();
            }
            else
            {
                cart = JsonConvert.DeserializeObject<List<Product>>(cartJson)!;
            }

            cart.Add(productToAdd);

            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            return RedirectToPage("/Cart");
        }
    }
}
