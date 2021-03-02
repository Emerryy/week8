using RestSharp;
using System;
using PetInfoServer.Models;

namespace PetInfoServer.APIServices
{
    public class NewsAPIService
    {
        private readonly string API_START = @"http://newsapi.org/v2/everything?q=";
        private readonly string API_END = @"&apiKey=8bee824932ee4380af56611454a2a59f";
        private readonly RestClient client = new RestClient();

        private News news;

        public News GetPetNews()
        {
            RestRequest request = new RestRequest(API_START + "pets" + API_END);
            IRestResponse<News> response = client.Get<News>(request);
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
                try
                {

                    //add news stories
                    News news = response.Data;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred - getting news stories " + ex.Message);
                }

                return news;
            }
        }

    }
}
