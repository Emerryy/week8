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
        private string sqlGetReceivTransfers = "SELECT * FROM transfers WHERE account_to = @accountTo";
        private string sqlGetTransDetails = "SELECT * from transfers WHERE transfer_id = @transferId";
        private string sqlTempAllTransfers = "SELECT * FROM transfers";


        private string sqlAddToTransfers = "INSERT INTO transfers(transfer_type_id, transfer_status_id, account_from, account_to, amount) VALUES(1001, 2001, @accountFrom, @accountTo, @dollarAmount) ";


        public TransferDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public List<Transfer> SentTransfers() //int accountFrom

        {
            List<Transfer> sent = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlTempAllTransfers, conn);
                    //cmd.Parameters.AddWithValue("@accountFrom", accountFrom);
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
                Console.WriteLine(ex.Message + "Problem in the sent transfers");
                return sent;

            }

            return sent;
        }

        public List<Transfer> ReceivedTransfers() //int accountTo
        {
            List<Transfer> received = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlTempAllTransfers, conn);
                    //cmd.Parameters.AddWithValue("@accountTo", accountTo);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer temp = GetTransferFromReader(reader);
                        received.Add(temp);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message + "Problem in the received transfers");
                return received;

            }

            return received;
        }

        public Transfer TransferDetails(int transferId)
        {
            Transfer transfer = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetTransDetails, conn);
                    cmd.Parameters.AddWithValue("@transferId", transferId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    
                        if (reader.HasRows && reader.Read())
                        {
                            transfer = GetTransferFromReader(reader);
                        }
                    
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message + "Problem in the transfer details");
                return transfer;

            }

            return transfer;
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

