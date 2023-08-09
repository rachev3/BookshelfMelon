using static System.Net.WebRequestMethods;

namespace MelonBookshelf.Models
{
    public class CityTemperature
    {
        public CityTemperature(WeatherData weatherData)
        {
            CityName = weatherData.Name;
            Temperature = weatherData.Main.TempCelsius;
            Icon = "https://openweathermap.org/img/wn/" + weatherData.Weather[0].Icon + ".png";
        }
        public string CityName { get; set; }
        public float Temperature { get; set; }
        public string Icon { get; set; }
    }
}
