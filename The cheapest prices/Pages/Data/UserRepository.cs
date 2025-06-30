using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace The_cheapest_prices.Pages.Data
{
    public class UserRepository
    {
        private readonly DataBase _dataBase;

        public UserRepository()
        {
            _dataBase = new DataBase();
        }

        public void GetUserForLogin(User user, DataTable table) 
        {
            using var connection = _dataBase.getConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `email` = @e AND `password` = @p", connection);
            command.Parameters.Add("@e", MySqlDbType.VarChar).Value = user.Email;
            command.Parameters.Add("@p", MySqlDbType.VarChar).Value = user.Password;

            adapter.SelectCommand = command;
            adapter.Fill(table);
        }

        public int PostUserForRegistration(User user)
        {
            using var connection = _dataBase.getConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `user` (`id`, `name`, `surname`, `sex`, `numberphone`, `email`, `dateOfBirth`, `password`) VALUES (@id, @na, @su, @se, @nu, @em, @da, @pa)", connection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = user.Id;
            command.Parameters.Add("@na", MySqlDbType.VarChar).Value = user.Name;
            command.Parameters.Add("@su", MySqlDbType.VarChar).Value = user.Surname;
            command.Parameters.Add("@se", MySqlDbType.VarChar).Value = user.Sex;
            command.Parameters.Add("@nu", MySqlDbType.VarChar).Value = user.NumberPhone;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = user.Email;
            command.Parameters.Add("@da", MySqlDbType.Date).Value = user.DateOfBirth.HasValue
                ? user.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;
            command.Parameters.Add("@pa", MySqlDbType.VarChar).Value = user.Password;

            _dataBase.openDB();
            return command.ExecuteNonQuery();
        }


        public void UpdateUserForEditProfile(User user) 
        {
            using var connection = _dataBase.getConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("UPDATE `user` SET `name` = @na, `surname` = @su, `numberphone` = @nu, `sex` = @se, `dateofbirth` = @da WHERE `id` = @id;", connection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = user.Id;
            command.Parameters.Add("@na", MySqlDbType.VarChar).Value = user.Name;
            command.Parameters.Add("@su", MySqlDbType.VarChar).Value = user.Surname;
            command.Parameters.Add("@nu", MySqlDbType.VarChar).Value = user.NumberPhone;
            command.Parameters.Add("@se", MySqlDbType.VarChar).Value = user.Sex;
            command.Parameters.Add("@da", MySqlDbType.Date).Value = user.DateOfBirth.HasValue
                ? user.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
                : DBNull.Value;

            connection.Open();
            command.ExecuteNonQuery();

        }

        public User GetUserById(int id)
        {
            using var connection = _dataBase.getConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `id` = @id", connection);
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

            adapter.SelectCommand = command;

            _dataBase.openDB();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Surname = reader.GetString("surname"),
                    Sex = reader.GetString("sex"),
                    DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime("dateofbirth")),
                    NumberPhone = reader.GetString("numberphone"),
                    Email = reader.GetString("email")
                };
            }

            return null;
        }
        public List<User> GetAllUsersExcept(int currentUserId)
        {
            var users = new List<User>();

            using var connection = _dataBase.createConnection();
            connection.Open();

            using var command = new MySqlCommand("SELECT * FROM user WHERE id != @currentUserId", connection);
            command.Parameters.AddWithValue("@currentUserId", currentUserId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name") 
                                                   
                });
            }

            return users;
        }

    }
}

