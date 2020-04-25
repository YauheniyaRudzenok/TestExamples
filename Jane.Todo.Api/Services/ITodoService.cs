using System.Collections.Generic;
using Jane.Todo.Api.Dto;

namespace Jane.Todo.Api.Services
{
	public interface ITodoService
	{
		TodoTaskDto Create(TodoTaskDto dto);
		TodoTaskDto Update(TodoTaskDto dto);
		TodoTaskDto GetById(int id);
		void Delete(int id);
		ICollection<TodoTaskDto> GetAll();
		void SetFinished(int id, bool finished);
	}
}