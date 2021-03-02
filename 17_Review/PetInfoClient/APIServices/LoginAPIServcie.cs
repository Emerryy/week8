using PetInfoClient.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace PetInfo.APIServices
{
    public class LoginAPIService
    {
        private readonly string API_URL = "https://localhost:44349/";

        protected static RestClient client = new RestClient();
        protected static APIUser user = new APIUser();

        public bool LoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }

        public bool Register(LoginUser registerUser)
        {
            RestRequest request = new RestRequest(API_URL + "register");
            request.AddJsonBody(registerUser);
            IRestResponse<APIUser> response = client.Post<APIUser>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("An error occurred communicating with the server.");
            }
            else if (!response.IsSuccessful)
            {
                if (response.Data != null && !string.IsNullOrWhiteSpace(response.Data.Message))
                {
                    throw new Exception("An error message was received: " + response.Data.Message);
                }
                else
                {
                    throw new Exception("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }
            }
            else
            {
                //todo 
                return true;
            }
        }

        public bool Login(string submittedName, string submittedPass)
        {
            var credentials = new { username = submittedName, password = submittedPass }; //this gets converted to JSON by RestSharp
            RestRequest request = new RestRequest(API_URL + "login");
            request.AddJsonBody(credentials);
            IRestResponse<APIUser> response = client.Post<APIUser>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("An error occurred communicating with the server.");
            }
            else if (!response.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(response.Data.Message))
                {
                    throw new Exception("An error message was received: " + response.Data.Message);
                }
                else
                {
                    throw new Exception("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }
            }
            else
            {
                user.Token = response.Data.Token;
                client.Authenticator = new JwtAuthenticator(user.Token);
                return true;
            }
        }

        public void Logout()
        {
            user = new APIUser();
            client.Authenticator = null;
        }

    }
}
