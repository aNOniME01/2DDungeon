﻿using MySql.Data.MySqlClient;
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
        private static bool IsConnected = false;
        public static void Connect()
        {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = "34.65.208.119",
                UserID = "player",
                Password = "TtE7q4h'*%&=~4)#",
                Database = "scoreboard",
                SslMode = MySqlSslMode.Disabled,
            };

            connection = new MySqlConnection();
            if (!IsConnected)
            {
                try
                {
                    connection.ConnectionString = csb.ConnectionString;
                    connection.Open();
                    IsConnected = true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        public static void Disconnect()
        {
            if (IsConnected)
            {
                connection.Close();
                IsConnected = false;
            }
        }
        public static string ReturnLastId()
        {
            var cmd = new MySqlCommand("select Id from Player order by Id desc limit 1;", connection);
            return cmd.ExecuteScalar().ToString();
        }
        public static void CreatePlayer(string username, string password)
        {
            if (!IsInDatabase("UserName", "Player", username))
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
        public static bool IsInDatabase(string type, string table, string sText)
        {
            string sql = $"SELECT {type} FROM {table}";
            var cmd = new MySqlCommand(sql, connection);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetString(0) == sText)
                {
                    rdr.Close();
                    return true;
                }
            }
            rdr.Close();
            return false;
        }
        public static string CheckPassword(string sText)
        {
            string sql = $"SELECT Password FROM Player WHERE UserName = \'{sText}\'";
            var cmd = new MySqlCommand(sql, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            string ret = rdr.GetString(0);
            rdr.Close();
            return ret;
        }
    }
}
