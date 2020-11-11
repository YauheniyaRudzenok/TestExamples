using System;
using System.Collections.Generic;
using System.Text;
using Jane.Todo.Dto;
using Jane.UI.Tests.TestServices;
using RestSharp;

namespace Jane.API.Tests
{
	public class RequestBuilder
	{
		private readonly TodoTaskDto task;
		public RequestBuilder()
		{
			task = new TodoTaskDto();
		}

		public RequestBuilder AddCreatedDate()
		{
			task.Created = DateTimeOffset.UtcNow;
			return this;
		}
		public RequestBuilder AddDueDate()
		{
			task.DueDate = Randoms.GenerateRandomDate();
			return this;
		}
		public RequestBuilder AddTitle()
		{
			task.Title = Randoms.GenerateStringValueInRange(5, 250);
			return this;
		}
		public RequestBuilder AddNote()
		{
			task.Note = Randoms.GenerateStringValueInRange(5, 250);
			return this;
		}
		public TodoTaskDto Build() => task;
	}
}
