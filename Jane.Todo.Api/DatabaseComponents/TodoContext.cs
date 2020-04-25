using Jane.Todo.Api.Entities;

using Microsoft.EntityFrameworkCore;

namespace Jane.Todo.Api.DatabaseComponents
{
	public class TodoContext : DbContext
	{
		private TodoContext(DbContextOptions<TodoContext> options)
			:base(options)
		{ }

		public DbSet<TodoTask> TodoTasks { get; set; }

		public static TodoContext CreateContext()
		{
			var options = new DbContextOptionsBuilder<TodoContext>()
				.UseInMemoryDatabase(databaseName: "Todo")
				.Options;

			return new TodoContext(options);
		}
	}
}