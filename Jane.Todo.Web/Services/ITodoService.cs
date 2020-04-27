using System.Collections.Generic;
using System.Threading.Tasks;
using Jane.Todo.Dto;

namespace Jane.Todo.Web.Services
{
	public interface ITodoService
	{
		Task<TodoTaskDto> Create(TodoTaskDto todoTaskDto);
		Task Delete(int id);
		Task<TodoTaskDto> Get(int id);
		Task<ICollection<TodoTaskDto>> GetAll();
		Task<TodoTaskDto> Update(TodoTaskDto todoTaskDto);
	}
}