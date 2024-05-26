using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DatabasePerp
{
    internal class DatabaseOperator
    {
        // database adress
        private static string db_adress = "SERVER=localhost;DATABASE=nauka_crud;UID=root;PASSWORD=;";
        // from MySqlConnector NuGet
        private static MySqlConnection connector = new(db_adress);

        public Boolean Insert(string login, string password, DateTime birthday)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || DateTime.MinValue == birthday)
                return false;

            try
            {
                connector.Open();
                string query = "INSERT INTO user(login, password, birthday) VALUES (@Login, @Password, @Birthdate)";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Birthdate", birthday);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - error message - " + Environment.NewLine + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Update(string login, string password, DateTime birthday)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || DateTime.MinValue == birthday)
                return false;

            try
            {
                connector.Open();
                string query = "SELECT id FROM user WHERE login=@Login";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Login", login);
                int userID = Convert.ToInt32(command.ExecuteScalar());
                if (userID > 0)
                {
                    command.Parameters.Clear();
                    query = "UPDATE user SET login=@Login,password=@Password,birthday=@Birthdate WHERE id=@Id";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@Id", userID);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Birthdate", birthday);
                    command.ExecuteNonQuery();
                    return true;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - error message - " + Environment.NewLine + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public Boolean Delete(string login)
        {
            if (string.IsNullOrEmpty(login))
                return false;

            try
            {
                connector.Open();
                string query = "DELETE FROM user WHERE login=@Login";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Login", login);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - error message - " + Environment.NewLine + ex.Message);
                return false;
            }
            finally
            {
                connector.Close();
            }
        }

        public List<User> Select()
        {
            try
            {
                List<User> users = new();
                connector.Open();
                string query = "SELECT * FROM user";
                using MySqlCommand command = new(query, connector);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        Birthday = reader.GetDateTime(3)
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - error message - " + Environment.NewLine + ex.Message);
                return new List<User>();
            }
            finally
            {
                connector.Close();
            }
        }

        public List<User> Select(string login)
        {
            try
            {
                List<User> users = new();
                connector.Open();
                string query = "SELECT * FROM user WHERE login=@Login";
                using MySqlCommand command = new(query, connector);
                command.Parameters.AddWithValue("@Login", login);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        Birthday = reader.GetDateTime(3)
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - error message - " + Environment.NewLine + ex.Message);
                return new List<User>();
            }
            finally
            {
                connector.Close();
            }
        }
    }
}
