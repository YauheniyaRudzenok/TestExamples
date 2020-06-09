using System;
using System.Threading.Tasks;
using Jane.Todo.Dto;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using RestSharp.Authenticators;

namespace Jane.UI.Tests
{
	class TasksModificationTests
	{
		private IWebDriver driver;
		private string token;
		private int taskId;

		[OneTimeSetUp]
		public void Autorize()
		{

			var auth = new Authentication();
			token = auth.LogIn();
			driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			loginPage.NavigateAndLogin();

		}

		[SetUp]
		public async Task Setup()
		{
			var client = new RestClient ("http://localhost:63558");
			client.Authenticator = new JwtAuthenticator(token);
			var request = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			request.AddJsonBody(new TodoTaskDto { 
				Created = DateTimeOffset.UtcNow, 
				DueDate = Randoms.GenerateRandomDate(),
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
			//Arrange
			var viewTask = new ViewTaskPage(driver);

			//Act
			viewTask.NavigateToViewPage(taskId);
			viewTask.WaitForPageToBeLoaded();
			viewTask.ClickEditButton();
			var editPage = new AddEditTaskPage(driver);
			editPage.WaitForPageToBeLoaded();
			editPage.ClearTheData();
			editPage.CheckFinishedCheckbox();
			editPage.PopulateAllItemsAndSubmit();
			viewTask.WaitForPageToBeLoaded();

			//Assert
			viewTask.EnsurePageLoaded();
			Assert.IsTrue(editPage.EnsureAllItemsAreSavedCorrectly());
			viewTask.CheckFinishedStatusIsCorrect(false);

		}

		[OneTimeTearDown]
		public void TearDown()
		{
			driver.Dispose();
		}

	}
}
