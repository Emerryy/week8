using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.APIClients
{
    public class TransferAPI : AuthService
    {

        private AccountAPI accountAPI = new AccountAPI();
        private readonly string API_URL = @"https://localhost:44315/transfer";

        public List<Transfer> GetTransfers()
        {
            List<Transfer> transfers = new List<Transfer>();
            RestRequest request = new RestRequest(API_URL);
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

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

        public List<JoinedTransfer> GetTransfersByUserId(/*int userId*/)
        {
            List<JoinedTransfer> transfers = new List<JoinedTransfer>();
            RestRequest request = new RestRequest(API_URL);
            IRestResponse<List<JoinedTransfer>> response = client.Get<List<JoinedTransfer>>(request);

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

        public bool AddTransfer(Transfer transfer)
        {
            RestRequest request = new RestRequest(API_URL);
            request.AddJsonBody(transfer);

            IRestResponse response = client.Post(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                return false;
            }
            else if (!response.IsSuccessful)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
