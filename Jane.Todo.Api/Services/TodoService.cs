using System.Collections.Generic;
using System.Linq;

using Jane.Todo.Api.DatabaseComponents;
using Jane.Todo.Dto;

namespace Jane.Todo.Api.Services
{
	public class TodoService : ITodoService
	{
		public TodoTaskDto Create(TodoTaskDto dto)
		{
			var entity = dto.ToEntity();

			using (var context = TodoContext.CreateContext())
			{
				context.TodoTasks.Add(entity);
				context.SaveChanges();
			}

			return entity.ToDto();
		}

		public void Delete(int id)
		{
			using var context = TodoContext.CreateContext();

			var todo = context.TodoTasks.First(x => x.Id == id);
			context.TodoTasks.Remove(todo);
			context.SaveChanges();
		}

		public ICollection<TodoTaskDto> GetAll()
		{
			using var context = TodoContext.CreateContext();

			return context.TodoTasks.Select(x => x.ToDto()).ToList();
		}

		public TodoTaskDto GetById(int id)
		{
			using var context = TodoContext.CreateContext();

			return context.TodoTasks.First(x => x.Id == id).ToDto();
		}

		public TodoTaskDto Update(TodoTaskDto dto)
		{
			using var context = TodoContext.CreateContext();

			var exists = context.TodoTasks.First(x => x.Id == dto.Id);
			exists.DueDate = dto.DueDate;
			exists.Finished = dto.Finished;
			exists.Note = dto.Note;
			exists.Title = dto.Title;

			context.SaveChanges();

			return exists.ToDto();
		}

		public void SetFinished(int id, bool finished)
		{
			using var context = TodoContext.CreateContext();

			var exists = context.TodoTasks.First(x => x.Id == id);
			exists.Finished = finished;

			context.SaveChanges();
		}
	}
}