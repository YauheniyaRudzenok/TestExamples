using Microsoft.Extensions.Configuration;

namespace Jane.Tests.Infrastructure
{
	public class Config
	{
		public IConfigurationRoot BuildConfig()
		{
			return new ConfigurationBuilder()
							.AddJsonFile("appsettings.json")
							.Build();
		}
	}
}
