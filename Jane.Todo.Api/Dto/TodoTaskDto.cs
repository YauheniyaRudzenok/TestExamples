using System;

namespace Jane.Todo.Api.Dto
{
	public class TodoTaskDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Note { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset DueDate { get; set; }
		public bool Finished { get; set; }
	}
}