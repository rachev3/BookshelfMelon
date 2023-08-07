using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace MelonBookshelf.Data.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<WeatherData> GetById(string cityName)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                string apiKey = "15f411ae608fb8661399e3a291cbffe5";
                HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();


                    WeatherData item = JsonConvert.DeserializeObject<WeatherData>(content);



              
                        return item;
                  
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
