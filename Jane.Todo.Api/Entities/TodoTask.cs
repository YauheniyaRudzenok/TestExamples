using System;

namespace Jane.Todo.Api.Entities
{
	public class TodoTask
	{
		public TodoTask()
		{
			this.Finished = false;
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Note { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset DueDate { get; set; }
		public bool Finished { get; set; }
	}
}