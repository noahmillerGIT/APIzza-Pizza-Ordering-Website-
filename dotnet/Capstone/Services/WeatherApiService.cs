using Capstone.Services.Model;
using RestSharp;
using System;

namespace Capstone.Services
{
    public class WeatherApiService : IWeather
    {
        private const string API_URL = "http://api.weatherapi.com/v1/current.json";
        private const string API_KEY = "18d67ad2c126487ea3343917232503";
        private readonly RestClient client = new RestClient();


        public Weather GetLiveWeather(string location)
        {
            RestRequest request = new RestRequest(API_URL + "?key=" + API_KEY);
            request.AddParameter("q", location);
            request.AddParameter("aqi", "no");

            IRestResponse<Weather> response = client.Get<Weather>(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.IsSuccessful)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

    }
}
