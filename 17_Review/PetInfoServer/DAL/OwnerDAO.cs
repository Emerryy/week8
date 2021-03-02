using PetInfoClient.Models;
using PetInfoServer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PetInfoServer.DAL
{
    public class OwnerDAO : IOwnerDAO
    {
        private string connectionString;
        private string sqlGetOwners = "SELECT * FROM owners;";

        public OwnerDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Owner> GetOwners()
        {
            List<Owner> owners = new List<Owner>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlGetOwners, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Owner owner = ReaderToOwner(reader);
                        owners.Add(owner);

                    }
                }
            }
            catch (Exception ex)
            {
                owners = new List<Owner>();
            }

            return owners;
        }

        private Owner ReaderToOwner(SqlDataReader reader)
        {
            Owner owner = new Owner();
            owner.Id = Convert.ToInt32(reader["owner_id"]);
            owner.FullName = Convert.ToString(reader["full_name"]);
            owner.PhoneNumber = Convert.ToString(reader["phone"]);
            owner.Email = Convert.ToString(reader["email"]);
            
            return owner;
        }
    }
}
