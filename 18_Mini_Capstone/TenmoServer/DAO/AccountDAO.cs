using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private string connectionString;
        public string sqlGetBalance = "SELECT balance FROM accounts WHERE @accountId = accounts.account_id";

        public AccountDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Account GetBalance(int accountId)
        {
            Account returnAccount = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlGetBalance, conn);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        returnAccount = ReaderToAccount(reader);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return returnAccount;
        }

        private Account ReaderToAccount(SqlDataReader reader)
        {
            Account account = new Account();
            account.AccountId = Convert.ToInt32(reader["account_id"]);
            account.UserId = Convert.ToInt32(reader["user_id"]);
            account.Balance = Convert.ToDecimal(reader["balance"]);
            return account;
        }
    }
}
