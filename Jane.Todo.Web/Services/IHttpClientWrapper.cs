using System.Net.Http;
using System.Threading.Tasks;

namespace Jane.Todo.Web.Services
{
	public interface IHttpClientWrapper
	{
		Task<T> GetAsync<T>(string requestUri);
		Task<HttpResponseMessage> PostAsync<TIn>(string requestUri, TIn value);
		Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn value);
		Task<TOut> PutAsync<TIn, TOut>(string requestUri, TIn value);
		Task DeleteAsync(string requestUri);
		IHttpClientWrapper AppendJwtToken();
	}
}