using Microsoft.AspNetCore.Components.Web;
using The_cheapest_prices.Pages.Data;

namespace The_cheapest_prices.Pages.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public string? ValidateProduct(Product product)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(product.Title) || product.Title.Length > 100)
                errors.Add("Назва товару обов'язкова і має бути до 100 символів.");

            if (string.IsNullOrWhiteSpace(product.Description) || product.Description.Length > 1000)
                errors.Add("Опис обов'язковий і має бути до 1000 символів.");

            if (product.Price <= 0)
                errors.Add("Ціна має бути більше нуля.");

            if (string.IsNullOrWhiteSpace(product.City) || product.City.Length > 100)
                errors.Add("Місто обов'язкове і має бути до 100 символів.");

            if (string.IsNullOrWhiteSpace(product.Address) || product.Address.Length > 200)
                errors.Add("Адреса обов'язкова і має бути до 200 символів.");

            if (product.Seller_Id <= 0)
                errors.Add("Некоректний ID продавця.");

            if (string.IsNullOrWhiteSpace(product.Image_Url) || product.Image_Url.Length > 300)
                errors.Add("Посилання на зображення обов'язкове і має бути до 300 символів.");

            if (string.IsNullOrWhiteSpace(product.Category) || product.Category.Length > 100)
                errors.Add("Категорія обов'язкова і має бути до 100 символів.");

            if (errors.Any())
                return string.Join("; ", errors);

            return null;
        }

        public void SaveProduct(Product product)
        {
            _productRepository.PostProducts(product);
        }
    }
}
