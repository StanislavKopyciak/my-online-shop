using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using The_cheapest_prices.Pages.Data;

namespace The_cheapest_prices.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ProductRepository _productRepository;

        public IndexModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public List<string> Categories { get; } = new List<string>
    {
        "Електрика",
        "Побутова техніка",
        "Інструменти та автотовари",
        "Сантехніка та ремонт",
        "Спорт і захоплення",
        "Одяг, взуття та прикраси",
        "Краса та здоров’я",
        "Дитячі товари",
        "Зоотовари",
        "Офіс, школа, книги"
    };

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                Products = _productRepository.GetProductsByCategory(SelectedCategory);
            }
            else
            {
                Products = _productRepository.GetAllProducts();
            }
        }
    }
}
