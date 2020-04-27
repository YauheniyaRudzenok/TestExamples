using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Jane.Todo.Web.Services
{
	public class HttpClientWrapper : IHttpClientWrapper
	{
		private IHttpContextAccessor httpContextAccessor;

		public HttpClientWrapper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
		{
			this.HttpClient = httpClient;
			this.httpContextAccessor = httpContextAccessor;
		}

		protected HttpClient HttpClient { get; private set; }

		public async Task DeleteAsync(string requestUri)
		{
			var message = await this.HttpClient.DeleteAsync(requestUri);
			message.EnsureSuccessStatusCode();
		}

		public async Task<T> GetAsync<T>(string requestUri)
		{
			var message = await this.HttpClient.GetAsync(requestUri);
			message.EnsureSuccessStatusCode();

			return await message.ReadContentAs<T>();
		}

		public Task<HttpResponseMessage> PostAsync<TIn>(string requestUri, TIn value)
		{
			return this.HttpClient.PostAsync(requestUri, new JsonContent(value));
		}

		public async Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn value)
		{
			var message = await this.HttpClient.PostAsync(requestUri, new JsonContent(value));
			message.EnsureSuccessStatusCode();

			return await message.ReadContentAs<TOut>();
		}

		public async Task<TOut> PutAsync<TIn, TOut>(string requestUri, TIn value)
		{
			var message = await this.HttpClient.PutAsync(requestUri, new JsonContent(value));
			message.EnsureSuccessStatusCode();

			return await message.ReadContentAs<TOut>();
		}

		public async Task<IHttpClientWrapper> AppendJwtToken()
		{
			if (this.HttpClient.DefaultRequestHeaders.Authorization != null)
				return this;

			var token = await this.httpContextAccessor.HttpContext.GetTokenAsync("jwt");

			this.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			return this;
		}
	}
}