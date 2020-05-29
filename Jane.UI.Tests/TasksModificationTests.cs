using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Jane.Todo.Dto;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;

namespace Jane.UI.Tests
{
	class TasksModificationTests
	{
		private IWebDriver driver;
		private string token;
		private int taskId;

		[OneTimeSetUp]
		public void BuildConfig()
		{
			var config = new Config();
			var configuration = config.BuildConfig();

			var client = new RestClient("http://localhost:63558");
			var request = new RestRequest("/api/auth", Method.POST);
			request.AddJsonBody(new AuthenticationRequestDto
			{
				UserName = configuration["appSettings:name"],
				Password = configuration["appSettings:password"]
			});

			var response = client.Post<AuthenticationResultDto>(request);
			token = response.Data.Token;
		}

		[SetUp]
		public async Task Setup()
		{
			driver = new ChromeDriver();

			var client = new RestClient ("http://localhost:63558");
			client.Authenticator = new JwtAuthenticator(token);
			var request = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			request.AddJsonBody(new TodoTaskDto { 
				Created = DateTimeOffset.UtcNow, 
				DueDate = DateTimeOffset.UtcNow.AddDays(3),
				Note = Randoms.GenerateStringValueInRange(5,250),
				Title = Randoms.GenerateStringValueInRange(1,250)
			});

			///GET
			//var response = client.Get<List<TodoTaskDto>>(request);

			///POST
			var response = client.Post<TodoTaskDto>(request);
			taskId = response.Data.Id;

		}

		[Test]
		public void EditExistedTask()
		{

		}

		[TearDown]
		public void TearDown()
		{
			driver.Dispose();
		}

	}
}
