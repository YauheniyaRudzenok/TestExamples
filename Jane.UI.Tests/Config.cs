using Microsoft.Extensions.Configuration;

namespace Jane.UI.Tests
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
