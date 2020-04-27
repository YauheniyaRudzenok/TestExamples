using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Jane.Todo.Web.Services
{
	public class JsonContent : StringContent
	{
		public JsonContent(object obj)
			: base(Serialize(obj), Encoding.UTF8, "application/json")
		{ }

		private static string Serialize(object content)
		{
			return JsonSerializer.Serialize(content);
		}
	}
}