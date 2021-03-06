using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.APIClients
{
    class AccountAPI : AuthService
    {
        private readonly string API_URL = @"https://localhost:44315/account";
        //client.Authenticator = new JwtAuthenticator(response.Data.Token);
        //add method for setting authentication token
        //get the token to API to allow it to authorize
        //either add method or get getBalance to take in the JWT
        //UserService.Token 


        public Account GetAccount(int userId)
        {
            Account account = new Account();
            RestRequest request = new RestRequest(API_URL + "/" + userId);
            IRestResponse<Account> response = client.Get<Account>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }

        public Account UpdateAccountFromBalance(int fromUserId, decimal amount)
        {
            Account fromAccount = GetAccount(fromUserId);

            fromAccount.AmountToTransfer = amount;


            RestRequest request = new RestRequest(API_URL + "/" + fromAccount.AccountId);

            request.AddJsonBody(fromAccount);

            IRestResponse<Account> response = client.Put<Account>(request);


            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }



    }
}
