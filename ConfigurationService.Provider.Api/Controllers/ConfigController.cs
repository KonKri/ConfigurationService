using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Provider.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetConfigForTenant([FromQuery] string? serviceId)
		{
			if (serviceId == "WeatherForecast")
				return Ok(@"{
					""Weather"": {
						""city"":""Athens""
					}
				}");



			return NotFound();
		}
	}
}
