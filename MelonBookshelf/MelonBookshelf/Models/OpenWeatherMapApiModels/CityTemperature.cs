namespace MelonBookshelf.Models
{
    public class CityTemperature
    {
        public CityTemperature(WeatherData weatherData)
        {
            CityName = weatherData.Name;
            Temperature = weatherData.Main.TempCelsius;
        }
        public string CityName { get; set; }
        public float Temperature { get; set; }
    }
}
