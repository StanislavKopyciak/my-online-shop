using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace The_cheapest_prices.Pages.Data
{
    public class DataBase
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=root;database=mydatabase");


        public void openDB()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closedDB()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }

        public MySqlConnection createConnection()
        {
            return new MySqlConnection("server=localhost;user=root;password=root;database=mydatabase");
        }
    }
}
