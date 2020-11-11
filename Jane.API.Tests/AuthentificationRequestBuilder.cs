using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;

namespace Jane.API.Tests
{
	public class AuthentificationRequestBuilder
	{
		private readonly AuthenticationRequestDto request;

		public AuthentificationRequestBuilder()
		{
			request = new AuthenticationRequestDto();
		}

		public AuthentificationRequestBuilder AddName()
		{
			request.UserName = Config.Instance["appCredentials:name"];
			return this;
		}

		public AuthentificationRequestBuilder AddPassword()
		{
			request.Password = Config.Instance["appCredentials:password"];
			return this;
		}

		public AuthenticationRequestDto Build() => request;
	}
}
