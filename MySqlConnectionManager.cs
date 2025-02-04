using MySql.Data.MySqlClient;
using System;

namespace PigeonInvadersAPI
{
    public class MySqlConnectionManager
    {
        private string connectionString = "Server=localhost;Database=PigeonInvadersDB;User ID=Traveler3114;Password=Jrmlj33185#;";

        public MySqlConnection OpenConnection()
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return connection;
        }

        public void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            Console.WriteLine("Connection closed.");
        }
    }
}
