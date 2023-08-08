using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.ViewComponents
{
    public class CityTemperatureViewComponent : ViewComponent
    {
        private readonly IWeatherService weatherService;
        private readonly IUserService userService;
        private string cityName;
        public CityTemperatureViewComponent(IWeatherService weatherService, IUserService userService)
        {
            this.weatherService = weatherService;
            this.userService = userService;
            cityName = "Sofia";
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string name = User.Identity.Name;
            if (name != null)
            {
                var userCity = userService.GetByUserName(name).Result.City;
                if (userCity != null) cityName = userCity;
            }


            var weatherData = await weatherService.GetById(cityName);
            CityTemperature cityTemperature = new CityTemperature(weatherData);
            var model = cityTemperature;
            return await Task.FromResult((IViewComponentResult)View("CityTemperature", model));
        }
    }
}
