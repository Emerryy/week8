using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.APIClients
{
    public class TransferAPI : AuthService
    {
        private readonly string API_URL = @"https://localhost:44315/transfer";

        public List<Tranfer> GetTransfers()

        {
            List<Tranfer> users = new List<Tranfer>();
            RestRequest request = new RestRequest(API_URL);
            IRestResponse<List<Tranfer>> response = client.Get<List<Tranfer>>(request);

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
