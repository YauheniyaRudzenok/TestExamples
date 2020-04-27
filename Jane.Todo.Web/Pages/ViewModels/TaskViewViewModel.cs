using System.Threading.Tasks;
using Jane.Todo.Dto;
using Jane.Todo.Web.Extensions;
using Jane.Todo.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jane.Todo.Web.Pages.ViewModels
{
	public class TaskViewViewModel
	{
		private readonly ITodoService todoService;
		private readonly NavigationManager navigationManager;

		public TaskViewViewModel(ITodoService todoService, NavigationManager navigationManager)
		{
			this.todoService = todoService;
			this.navigationManager = navigationManager;
		}

		public TodoTaskDto Task { get; set; }
		public bool ShowEditButton { get; private set; }

		public async Task RetrieveTaskAsync(int id, Task<AuthenticationState> authenticationState)
		{
			this.Task = await this.todoService.Get(id);
			this.ShowEditButton = await authenticationState.IsAuthenticated();
		}

		public void Edit()
		{
			this.navigationManager.NavigateTo($"/taskedit/{this.Task.Id}");
		}
	}
}