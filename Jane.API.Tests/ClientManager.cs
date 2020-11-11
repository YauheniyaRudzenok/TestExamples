using Jane.Tests.Infrastructure;
using RestSharp;

namespace Jane.API.Tests
{
	public class ClientManager
	{
		private readonly string pathInstance = Config.Instance["appSettings:apiURL"];
		private RestClient _client;

		public RestClient Client => _client ??= new RestClient(pathInstance);
	}
}
