using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using The_cheapest_prices.Pages.Data;

namespace The_cheapest_prices.Pages
{
    public class CartModel : PageModel
    {
        public List<Product> Cart { get; set; }

        public void OnGet()
        {
            var cartJson = HttpContext.Session.GetString("cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                Cart = new List<Product>();
            }
            else
            {
                Cart = JsonConvert.DeserializeObject<List<Product>>(cartJson);
            }
        }
    }
}
