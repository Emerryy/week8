using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;
using TenmoServer.DAO;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {

        private string connectionString;
        public string sqlGetAccounts = "SELECT * FROM accounts";
        private string sqlGetAccountByUser = "SELECT * FROM accounts WHERE user_id = @userId";
        private string sqlUpdateBalance = "UPDATE accounts SET balance = @adjustedBalance WHERE user_id = @userID";


        public AccountDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Account GetAccount(int userId)
        {

            Account returnAccount = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlGetAccountByUser, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account temp = ReaderToAccount(reader);
                        returnAccount = temp;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return returnAccount;
        }

        public List<Account> AllAccounts()
        {

            List<Account> accounts = new List<Account>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlGetAccounts, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account temp = ReaderToAccount(reader);
                        accounts.Add(temp);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return accounts;


        }

        public Account UpdateBalance(Account account)
        {
            decimal transferAmount = account.AmountToTransfer;
            decimal adjustedBalance = account.Balance - transferAmount;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlUpdateBalance, conn);
                    cmd.Parameters.AddWithValue("@userID", account.UserId);
                    cmd.Parameters.AddWithValue("@adjustedBalance", adjustedBalance);
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return account;
                    }


                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return account;
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
