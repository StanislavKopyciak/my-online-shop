using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using The_cheapest_prices.Pages.Data; 
using System.Collections.Generic;
using System.Linq;

namespace The_cheapest_prices.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ProductRepository _productRepository;

        public SearchModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

        public List<Product> Results { get; set; } = new List<Product>();

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(Query))
            {
                Results = new List<Product>();
                return;
            }

            Results = _productRepository.GetAllProducts()
                .Where(p => p.Title != null && p.Title.Contains(Query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

    }
}
