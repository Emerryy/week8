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
            //Account account = GetAccount(userID);   //AmountToTransfer isn't making it to here.

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
                        if (temp.UserId == account.UserId)
                        {
                            temp.Balance -= account.AmountToTransfer;   //This should be updating the SQL
                            account = temp;
                        }

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
