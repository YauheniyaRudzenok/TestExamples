using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jane.Todo.Dto;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Jane.UI.Tests
{
	class TasksModificationTests
	{
		private IWebDriver driver;

		[SetUp]
		public async Task Setup()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:63508");
			var taskDto = new TodoTaskDto();
			var content = new StringContent(JsonConvert.SerializeObject(taskDto), Encoding.UTF8, "application/json");
			var result = await client.PostAsync("/api/todo", content);
		}

		[Test]
		public void EditExistedTask()
		{

		}

	}
}
