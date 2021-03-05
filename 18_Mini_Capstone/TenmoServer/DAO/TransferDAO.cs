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
        private string sqlAddTransfer = "INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
            "VALUES(@TransferTypeId, @TransferStatusId, @AccountFrom, @AccountTo, @DollarAmount)";

        private string sqlGetTransferDetailsJoined = " SELECT transfers.transfer_id, fu.username fromUser, tu.username toUser, transfers.amount transferAmount FROM transfers " +
         "JOIN accounts f ON transfers.account_from = f.account_id " +
         "JOIN accounts t ON transfers.account_to = t.account_id " +
         "JOIN users fu ON f.user_id = fu.user_id " +
         "JOIN users tu ON t.user_id = tu.user_id " +
         "WHERE t.user_id = @userId OR f.user_id = @userId";


        private string sqlAddToTransfers = "INSERT INTO transfers(transfer_type_id, transfer_status_id, account_from, account_to, amount) VALUES(1001, 2001, @accountFrom, @accountTo, @dollarAmount) ";


        public TransferDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public List<JoinedTransfer> GetTransfersByUserId(int userId) 

        {
            List<JoinedTransfer> transferDetails = new List<JoinedTransfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetTransferDetailsJoined, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        JoinedTransfer temp = JoinedTransferFromReader(reader);
                        transferDetails.Add(temp);
                    }
                }
            }
            catch (SqlException ex)
            {
                
                return transferDetails;

            }

            return transferDetails;
        }

        public List<Transfer> GetTransfers() 
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
                Console.WriteLine(ex.Message + "Problem in the get all transfers");
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

        public bool AddTransfer(Transfer transfer)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlAddTransfer, conn);
                    cmd.Parameters.AddWithValue("@TransferTypeId", transfer.TransferTypeId);
                    cmd.Parameters.AddWithValue("@TransferStatusId", transfer.TransferStatusId);
                    cmd.Parameters.AddWithValue("@AccountFrom", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@AccountTo", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@DollarAmount", transfer.DollarAmount);
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }


        private JoinedTransfer JoinedTransferFromReader(SqlDataReader reader)
        {
            return new JoinedTransfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                FromUser = Convert.ToString(reader["fromUser"]),
                ToUser = Convert.ToString(reader["toUser"]),
                Type = "Send",
                Status = "Approved",
                Amount = Convert.ToDecimal(reader["transferAmount"]),
            };
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

