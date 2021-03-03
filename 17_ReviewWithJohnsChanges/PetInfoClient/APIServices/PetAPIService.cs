using PetInfo.APIServices;
using PetInfoClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;


namespace PetInfoClient.APIServices
{
    public class PetAPIService : LoginAPIService
    {
        private readonly string API_URL = "https://localhost:44349/pet/";

        public List<Pet> GetPets()
        {
            List<Pet> cities = new List<Pet>();

            RestRequest request = new RestRequest(API_URL);
            IRestResponse<List<Pet>> response = client.Get<List<Pet>>(request);
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

        public bool AddPet(Pet pet)
        {
            RestRequest request = new RestRequest(API_URL);
            request.AddJsonBody(pet);

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
