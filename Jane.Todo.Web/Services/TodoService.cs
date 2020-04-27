using System.Collections.Generic;
using System.Threading.Tasks;

using Jane.Todo.Dto;

namespace Jane.Todo.Web.Services
{
	public class TodoService : ITodoService
	{
		private readonly IHttpClientWrapper httpClient;

		public TodoService(IHttpClientWrapper httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<ICollection<TodoTaskDto>> GetAll() =>
			this.httpClient.GetAsync<ICollection<TodoTaskDto>>("api/todo");

		public Task<TodoTaskDto> Get(int id) =>
			this.httpClient.GetAsync<TodoTaskDto>($"api/todo/{id}");

		public async Task<TodoTaskDto> Create(TodoTaskDto todoTaskDto)
		{
			await this.httpClient.AppendJwtToken();

			return await this.httpClient.PostAsync<TodoTaskDto, TodoTaskDto>("api/todo", todoTaskDto);
		}

		public async Task<TodoTaskDto> Update(TodoTaskDto todoTaskDto)
		{
			await this.httpClient.AppendJwtToken();

			return await this.httpClient.PutAsync<TodoTaskDto, TodoTaskDto>("api/todo", todoTaskDto);
		}

		public async Task Delete(int id)
		{
			await this.httpClient.AppendJwtToken();
			await this.httpClient.DeleteAsync($"api/todo/{id}");
		}
	}
}