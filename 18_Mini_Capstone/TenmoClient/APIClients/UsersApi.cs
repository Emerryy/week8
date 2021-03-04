using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;


namespace TenmoClient.APIClients
{
    public class UsersAPI : AuthService
    {
        private readonly string API_URL = @"https://localhost:44315/users";

        public List<Users> GetUsers()

        {
            List<Users> users = new List<Users>();
            RestRequest request = new RestRequest(API_URL);
            IRestResponse<List<Users>> response = client.Get<List<Users>>(request);

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

