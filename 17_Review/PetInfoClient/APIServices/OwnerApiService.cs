using PetInfo.APIServices;
using PetInfoClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PetInfoClient.APIServices
{
    public class OwnerApiService : LoginAPIService
    {
        private readonly string API_URL = "https://localhost:44349/owner/";

        public List<Owner> GetOwners()
        {
            List<Owner> owners = new List<Owner>();

            RestRequest request = new RestRequest(API_URL);
            IRestResponse<List<Owner>> response = client.Get<List<Owner>>(request);
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

        //public bool AddPet(Pet pet)
        //{
        //    RestRequest request = new RestRequest(API_URL);
        //    request.AddJsonBody(pet);

        //    IRestResponse response = client.Post(request);
        //    if (response.ResponseStatus != ResponseStatus.Completed)
        //    {
        //        return false;
        //    }
        //    else if (!response.IsSuccessful)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}


    }
}
