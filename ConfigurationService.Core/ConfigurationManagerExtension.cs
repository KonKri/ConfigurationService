using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConfigurationService.Core
{
	public static class ConfigurationManagerExtension
	{
		public static IConfigurationBuilder AddConfigurationService(this IConfigurationBuilder builder, string serviceId)
	{
			builder.Sources.Add(new ConfigurationServiceSource(serviceId));
			return builder;
		}
	}

	public class ConfigurationServiceSource : JsonStreamConfigurationSource
	{

		public ConfigurationServiceSource(string serviceId)
		{
			// get config from another service.
			using var client = new HttpClient();
			var res = client.GetAsync($"https://localhost:7060/api/Config?serviceId={serviceId}")
				.GetAwaiter()
				.GetResult();

			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			var stream = res.Content.ReadAsStream();
			this.Stream = stream;
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			return new ConfigurationServiceProvider(this);
		}
	}

	public class ConfigurationServiceProvider : JsonStreamConfigurationProvider
	{
		public ConfigurationServiceProvider(JsonStreamConfigurationSource source) : base(source)
		{
		}


		//public ConfigurationServiceProvider(string serviceId)
		//{
		//	_serviceId = serviceId;
		//}


		//public override void Load()
		//{
		//	// get config from another service.
		//	using var client = new HttpClient();
		//	var res = client.GetAsync($"https://localhost:7060/api/Config?serviceId={_serviceId}")
		//		.GetAwaiter()
		//		.GetResult();

		//	var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

		//	var d = JsonConvert.DeserializeObject<JObject>(content);

		//	var dic = new Dictionary<string, string>();
		//	foreach (var prop in d.Properties())
		//	{
		//		dic.Add(prop.Name, prop.Value.ToString());
		//	}

		//	Data = dic;
		//}
	}
}