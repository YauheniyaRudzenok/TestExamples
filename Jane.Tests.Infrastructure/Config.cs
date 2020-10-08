using Microsoft.Extensions.Configuration;

namespace Jane.Tests.Infrastructure
{
	public static class Config
	{
		private static IConfigurationRoot configuration;
		private static object sync = new object();
		//public static IConfigurationRoot Instance()
		//{
		//	if (configuration == null)
		//	{
		//		configuration = new ConfigurationBuilder()
		//					.AddJsonFile("appsettings.json")
		//					.Build();
		//	}
		//	return configuration;
		//}
		public static IConfigurationRoot Instance
		{
			get
			{
				if (configuration == null)
				{
					lock (sync)
					{
						if (configuration == null)
							configuration = new ConfigurationBuilder()
								.AddJsonFile("appsettings.json")
								.Build();
					}
				}
				return configuration;
			}
		}
	}
}