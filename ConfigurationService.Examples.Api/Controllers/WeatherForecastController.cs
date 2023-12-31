using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Examples.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IConfiguration _config;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			var weatherConfig = _config.GetSection("Weather").Get<WeatherConfig>();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)],
				City = weatherConfig.City
			})
			.ToArray();
		}
	}
}