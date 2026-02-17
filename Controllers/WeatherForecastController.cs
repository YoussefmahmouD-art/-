using Microsoft.AspNetCore.Mvc;
using مشروع_قبل_الشغل.Services;

namespace مشروع_قبل_الشغل.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IWeatherServices weatherServices)
        {
            _logger = logger;
            WeatherServices = weatherServices;
        }

        public IWeatherServices WeatherServices { get; }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
           var services = new WetherForCastServices();
            return services.Get();
        }
    }
}
