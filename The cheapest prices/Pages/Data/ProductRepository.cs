using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace The_cheapest_prices.Pages.Data
{
    public class ProductRepository
    {
        private readonly DataBase _dataBase;

        public ProductRepository(DataBase dataBase) 
        {
            _dataBase = dataBase; 
        }
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using var connection = _dataBase.createConnection();
            connection.Open(); 

            using var command = new MySqlCommand("SELECT * FROM `product`", connection);
            using var reader = command.ExecuteReader(); 

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description"),
                    Price = reader.GetDecimal("price"),
                    With_Delivery = reader.GetByte("with_delivery"),
                    City = reader.GetString("city"),
                    Address = reader.GetString("address"),
                    Seller_Id = reader.GetInt32("seller_id"),
                    Created_At = reader.GetDateTime("created_at"),
                    Image_Url = reader.GetString("image_url"),
                    Category = reader.GetString("category"),
                    Available = reader.GetByte("available")
                });
            }

            return products;
        }

        public int PostProducts(Product products)
        {
            using var connection = _dataBase.createConnection();
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `product`(`id`, `title`, `description`, `price`, `with_delivery`, `city`, `address`, `seller_id`, `created_at`, `image_url`, `category`) " +
                "VALUES (@id, @ti, @de, @pr, @wi, @ci, @ad, @se, @cr, @im, @ca)", connection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = products.Id;
            command.Parameters.Add("@ti", MySqlDbType.VarChar).Value = products.Title;
            command.Parameters.Add("@de", MySqlDbType.Text).Value = products.Description;
            command.Parameters.Add("@pr", MySqlDbType.Decimal).Value = products.Price;
            command.Parameters.Add("@wi", MySqlDbType.Byte).Value = products.With_Delivery;
            command.Parameters.Add("@ci", MySqlDbType.VarChar).Value = products.City;
            command.Parameters.Add("@ad", MySqlDbType.VarChar).Value = products.Address;
            command.Parameters.Add("@se", MySqlDbType.Int32).Value = products.Seller_Id;
            command.Parameters.Add("@cr", MySqlDbType.DateTime).Value = DateTime.Now;
            command.Parameters.Add("@im", MySqlDbType.VarChar).Value = products.Image_Url;
            command.Parameters.Add("@ca", MySqlDbType.VarChar).Value = products.Category;
          

            adapter.SelectCommand = command;
            return command.ExecuteNonQuery();
        }

        public Product? GetProductById(int id)
        {
            using var connection = _dataBase.createConnection();
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `product` WHERE `id` = @id", connection);
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

            adapter.SelectCommand = command;

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return (new Product
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description"),
                    Price = reader.GetDecimal("price"),
                    With_Delivery = reader.GetByte("with_delivery"),
                    City = reader.GetString("city"),
                    Address = reader.GetString("address"),
                    Seller_Id = reader.GetInt32("seller_id"),
                    Created_At = reader.GetDateTime("created_at"),
                    Image_Url = reader.GetString("image_url"),
                    Category = reader.GetString("category"),
                    Available = reader.GetByte("available")
                });
            }

            return null;
        }

        public void UpdateProductAvailability(int productId, bool available)
        {
            using var connection = _dataBase.createConnection();
            connection.Open();

            using var command = new MySqlCommand("UPDATE product SET Available = @available WHERE id = @id", connection);
            command.Parameters.AddWithValue("@available", available ? 1 : 0);
            command.Parameters.AddWithValue("@id", productId);

            command.ExecuteNonQuery();
        }

        public List<Product> GetProductsByCategory(string category)
        {
            var products = new List<Product>();
            using var connection = _dataBase.createConnection();
            connection.Open();

            using var command = new MySqlCommand("SELECT * FROM product WHERE category = @category", connection);
            command.Parameters.AddWithValue("@category", category);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description"),
                    Price = reader.GetDecimal("price"),
                    With_Delivery = reader.GetByte("with_delivery"),
                    City = reader.GetString("city"),
                    Address = reader.GetString("address"),
                    Seller_Id = reader.GetInt32("seller_id"),
                    Created_At = reader.GetDateTime("created_at"),
                    Image_Url = reader.GetString("image_url"),
                    Category = reader.GetString("category"),
                    Available = reader.GetByte("available")
                });
            }

            return products;
        }

        public List<Product> GetProductsBySellerId(int sellerId)
        {
            var products = new List<Product>();

            using var connection = _dataBase.createConnection();
            connection.Open();

            using var command = new MySqlCommand("SELECT * FROM product WHERE seller_id = @sellerId AND available = 1", connection);
            command.Parameters.AddWithValue("@sellerId", sellerId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description"),
                    Price = reader.GetDecimal("price"),
                    With_Delivery = reader.GetByte("with_delivery"),
                    City = reader.GetString("city"),
                    Address = reader.GetString("address"),
                    Seller_Id = reader.GetInt32("seller_id"),
                    Created_At = reader.GetDateTime("created_at"),
                    Image_Url = reader.GetString("image_url"),
                    Category = reader.GetString("category"),
                    Available = reader.GetByte("available")
                });
            }

            return products;
        }
    }
}
