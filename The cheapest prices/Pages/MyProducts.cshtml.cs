using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using The_cheapest_prices.Pages.Data;

namespace The_cheapest_prices.Pages
{
    public class MyProductsModel : PageModel
    {
        private readonly ProductRepository _productRepository;

        public MyProductsModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> MyProducts { get; set; } = new List<Product>();

        [BindProperty]
        public int ProductIdToUpdate { get; set; }

        public void OnGet()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                MyProducts = new List<Product>();
                return;
            }

            MyProducts = _productRepository.GetProductsBySellerId(userId);
        }

        public IActionResult OnPostToggleAvailability(int productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            _productRepository.UpdateProductAvailability(productId, false);

            return RedirectToPage(); 
        }
    }
}