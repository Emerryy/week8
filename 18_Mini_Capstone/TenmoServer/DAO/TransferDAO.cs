using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;
namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        //public List<Transfer> SentTransfers();

        //public List<Transfer> ReceivedTransfers();

        //public Transfer TransferDetails(int transferId);


        private string connectionString;
        private string sqlGetSentTransfers = "SELECT * FROM transfers WHERE account_from = @accountFrom";

        public TransferDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Transfer> ReceivedTransfers(int accountTo)
        {
            throw new NotImplementedException();
        }

        public List<Transfer> SentTransfers(int accountFrom)
        {
            List<Transfer> sent = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetSentTransfers, conn);
                    cmd.Parameters.AddWithValue("@accountFrom", accountFrom);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer temp = GetTransferFromReader(reader);
                        sent.Add(temp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Problem in the sent transfers");
                return sent;
                //return ex.Message;
            }

            return sent;
        }

        public Transfer TransferDetails(int transferId)
        {
            throw new NotImplementedException();
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            return new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                DollarAmount = Convert.ToDecimal(reader["amount"]),
            };
        }
    }

}

