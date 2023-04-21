using Capstone.DTO;
using Capstone.Services;
using Capstone.Services.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeather weatherService;

        public WeatherController(IWeather _weatherService) 
        {
            this.weatherService = _weatherService;
        }

        [HttpGet("{location}")]
        public ActionResult<WeatherDto> GetLiveWeather(string location)
        {
            Weather weathers = weatherService.GetLiveWeather(location);

            WeatherDto basicWeatherInfo = new WeatherDto()
            {
                locationName = weathers.Location.Name,
                region = weathers.Location.Region,
                country = weathers.Location.Country,
                localTime = weathers.Location.LocalTime,
                text = weathers.Current.Condition.Text,
                icon = weathers.Current.Condition.Icon,
                temp_c = weathers.Current.Temp_c,
                temp_f = weathers.Current.Temp_f,
                humidity = weathers.Current.Humidity
            };
            if (basicWeatherInfo != null)
            {
                return Ok(basicWeatherInfo);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
