using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Jane.Todo.Dto;
using Jane.Todo.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Jane.Todo.Web.Pages.ViewModels
{
	public class TaskEditViewModel
	{
		private readonly ITodoService todoService;
		private readonly NavigationManager navigationManager;

		public TaskEditViewModel(ITodoService todoService, NavigationManager navigationManager)
		{
			this.todoService = todoService;
			this.navigationManager = navigationManager;
		}

		public EditForm TaskForm { get; set; }

		public async Task RetirieveTodoTaskAsync(int id)
		{
			if(id == 0)
			{
				this.TaskForm = new EditForm
				{
					Finished = false,
					DueDate = DateTimeOffset.Now
				};
			}
			else
			{
				var dto = await this.todoService.Get(id);

				this.TaskForm = TaskEditViewModel.ConvertToForm(dto);
			}
		}

		public async Task Save()
		{
			var dto = TaskEditViewModel.ConvertToDto(this.TaskForm);

			if(dto.Id == 0)
			{
				dto.Created = DateTimeOffset.UtcNow;
				dto = await this.todoService.Create(dto);
			}
			else
			{
				dto = await this.todoService.Update(dto);
			}

			this.navigationManager.NavigateTo($"/taskview/{dto.Id}");
		}

		public async Task Delete()
		{
			await this.todoService.Delete(this.TaskForm.Id);

			this.navigationManager.NavigateTo("/");
		}

		private static TodoTaskDto ConvertToDto(EditForm taskForm)
		{
			return new TodoTaskDto
			{
				Created = taskForm.Created,
				DueDate = taskForm.DueDate,
				Finished = taskForm.Finished,
				Id = taskForm.Id,
				Note = taskForm.Note,
				Title = taskForm.Title
			};
		}

		private static EditForm ConvertToForm(TodoTaskDto dto)
		{
			return new EditForm
			{
				Created = dto.Created,
				DueDate = dto.DueDate,
				Finished = dto.Finished,
				Id = dto.Id,
				Note = dto.Note,
				Title = dto.Title
			};
		}

		public class EditForm
		{
			public int Id { get; set; }
			[Required]
			[MinLength(1)]
			[MaxLength(250)]
			public string Title { get; set; }
			[Required]
			[MinLength(5)]
			[MaxLength(250)]
			public string Note { get; set; }
			public DateTimeOffset Created { get; set; }
			[Required]
			[DataType(DataType.Date)]
			public DateTimeOffset DueDate { get; set; }
			public bool Finished { get; set; }
		}
	}
}