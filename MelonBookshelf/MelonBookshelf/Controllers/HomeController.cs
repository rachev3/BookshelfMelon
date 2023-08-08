using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MelonBookshelf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService weatherService;
        private readonly IUserService userService;
        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, IUserService userService)
        {
            _logger = logger;
            this.weatherService = weatherService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            HttpContext httpContext = HttpContext;                        
            ViewData["HttpContext"] = httpContext;

            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}