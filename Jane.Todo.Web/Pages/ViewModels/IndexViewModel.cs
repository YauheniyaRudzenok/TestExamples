using System.Collections.Generic;
using System.Threading.Tasks;

using Jane.Todo.Dto;
using Jane.Todo.Web.Extensions;
using Jane.Todo.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jane.Todo.Web.Pages.ViewModels
{
	public class IndexViewModel
	{
		private readonly ITodoService todoService;

		public IndexViewModel(ITodoService todoService)
		{
			this.todoService = todoService;
		}

		public ICollection<TodoTaskDto> Tasks { get; set; }
		public bool ShowEditButton { get; private set; }

		public async Task RetrieveTasksAsync(Task<AuthenticationState> authenticationStateTask)
		{
			this.Tasks = await this.todoService.GetAll();
			this.ShowEditButton = await authenticationStateTask.IsAuthenticated();
		}
	}
}