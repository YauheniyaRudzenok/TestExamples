using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Jane.Todo.Web.Services
{
	public static class LocalStorageExtensions
	{
		private const string AuthTokenKey = "AUTH_TOKEN";

		public static Task<string> GetAuthToken(this ILocalStorageService localStorage)
		{
			return localStorage.GetItemAsync<string>(AuthTokenKey);
		}

		public static Task SetAuthToken(this ILocalStorageService localStorage, string token)
		{
			return localStorage.SetItemAsync(AuthTokenKey, token);
		}
	}
}