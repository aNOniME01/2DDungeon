using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDungeon
{
    internal class SQLOperations
    {
    
        private static MySqlConnection connection;
        public static void Connect(bool IsConnected)
        {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = "34.65.208.119",
                UserID = "test",
                Password = "",
                Database = "scoreboard",
                SslMode = MySqlSslMode.Disabled,
            };

            connection = new MySqlConnection();
            if (IsConnected)
            {
                connection.Close();
            }
            else
            {
                try
                {
                    connection.ConnectionString = csb.ConnectionString;
                    connection.Open();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public static void Disconnect()
        {
            Connect(true);
        }
        public static string ReturnLastId()
        {
            var cmd = new MySqlCommand("select Id from Player order by Id desc limit 1;", connection);
            return cmd.ExecuteScalar().ToString();
        }
        public static void CreatePlayer(string username, string password)
        {
            if (SearchFor("UserName","Player",username))
            {
                int id = Convert.ToInt32(ReturnLastId()) + 1;

                var sql = "INSERT INTO Player(Id, UserName, Password) VALUES(@id, @username, @password)";
                using var cmd = new MySqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }
        private static bool SearchFor(string type,string table,string sText)
        {
            string sql = $"SELECT {type} FROM {table}";
            using var cmd = new MySqlCommand(sql, connection);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetString(0) == sText)
                {
                    MessageBox.Show($"\"{sText}\" is aleardy inside the database");
                    return false;
                }
            }
            return true;
        }
    }
}
