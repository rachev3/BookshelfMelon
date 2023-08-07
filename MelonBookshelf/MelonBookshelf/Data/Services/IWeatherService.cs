using MelonBookshelf.Models;

namespace MelonBookshelf.Data.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetById(string cityName);
    }
}
