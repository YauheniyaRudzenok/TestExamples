using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jane.Todo.Web.Services
{
	public static class HttpResponseMessageExtensions
	{
		public static async Task<T> ReadContentAs<T>(this HttpResponseMessage message)
		{
			return Deserialize<T>(await message.Content.ReadAsStringAsync());
		}

		private static T Deserialize<T>(string content)
		{
			return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}
	}
}