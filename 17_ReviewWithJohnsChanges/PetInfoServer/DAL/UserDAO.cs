using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PetInfoServer.Models;
using PetInfoServer.Security;
using PetInfoServer.Security.Models;

namespace PetInfoServer.DAL
{
    public class UserDAO: IUserDAO
    {
        private readonly string connectionString;

        const string sql_getUser = "SELECT user_id, username, password_hash, salt FROM users WHERE username = @username";
        const string sql_getUsers = "SELECT user_id, username, password_hash, salt FROM users";
        const string sql_addUser = "INSERT INTO users (username, password_hash, salt) VALUES (@username, @password_hash, @salt)";

        public UserDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_getUser, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return returnUser;
        }

        public User AddUser(string username, string password)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql_addUser, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return GetUser(username);
        }


        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                Id = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"])
            };

            return u;
        }

    }
}
