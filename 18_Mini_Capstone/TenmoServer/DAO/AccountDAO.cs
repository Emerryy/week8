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
        private string sqlGetAccountByUser = "SELECT account_id FROM accounts WHERE user_id = @userId";

        public AccountDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Account GetBalance(int userId)
        {

            Account returnAccount = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlGetAccounts, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
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
