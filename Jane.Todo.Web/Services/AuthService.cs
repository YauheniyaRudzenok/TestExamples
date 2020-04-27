using System.Threading.Tasks;
using Jane.Todo.Dto;

namespace Jane.Todo.Web.Services
{
	public class AuthService : IAuthService
	{
		private readonly IHttpClientWrapper httpClient;

		public AuthService(IHttpClientWrapper httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<AuthenticationResultDto> SignIn(string userName, string password)
		{
			var message = await this.httpClient.PostAsync(
				"api/auth",
				new AuthenticationRequestDto { UserName = userName, Password = password });

			return message.IsSuccessStatusCode
				? await message.ReadContentAs<AuthenticationResultDto>() 
				: null;
		}
	}
}