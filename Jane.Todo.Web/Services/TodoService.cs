using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jane.Todo.Web.Services
{
	public class TodoService : ITodoService
	{
		private readonly IHttpClientWrapper httpClient;

		public TodoService(IHttpClientWrapper httpClient)
		{
			this.httpClient = httpClient;
		}




	}
}