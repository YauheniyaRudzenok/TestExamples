using System;
using System.Threading.Tasks;
using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using RestSharp;
using RestSharp.Authenticators;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	class TasksModificationTests
	{
		private string token;
		private int taskId;
		private string taskTitle;

		[OneTimeSetUp]
		public void Autorize()
		{
			var auth = new Authentication();
			token = auth.LogIn();
		}

		[SetUp]
		public async Task Setup()
		{
			//navigate to login page
			Main.PageManager.LoginPage.NavigateAndLogin();

			//create new task
			taskTitle = Randoms.GenerateStringValueInRange(1, 250);

			var client = new RestClient(Config.Instance["appSettings:apiURL"]);
			client.Authenticator = new JwtAuthenticator(token);
			var request = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			request.AddJsonBody(new TodoTaskDto
			{
				Created = DateTimeOffset.UtcNow,
				DueDate = Randoms.GenerateRandomDate(),
				Note = Randoms.GenerateStringValueInRange(5, 250),
				Title = taskTitle
			});

			///GET
			//var response = client.Get<List<TodoTaskDto>>(request);

			// POST
			var response = client.Post<TodoTaskDto>(request);
			taskId = response.Data.Id;

		}

		[Test]
		public void EditExistedTask()
		{
			//Act
			Main.PageManager.ViewTaskPage.NavigateToViewPage(taskId);
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.ViewTaskPage.ClickEditButton();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.ClearTheData();
			Main.PageManager.AddEditTaskPage.CheckFinishedCheckbox();
			Main.PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			Main.PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
			Main.PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void OpenEditTaskFromTasksPage()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.ClickEditLatestCreatedTask();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.AllItemsArePresented());
		}

		[Test]
		public void OpenInfoTaskFromTasksPage()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.ClickInfoForLatestCreatedTask();

			//Assert
			Main.PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(Main.PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
		}

		[Test]
		public void EditTaskFromInfoPage()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.ClickInfoForLatestCreatedTask();
			Main.PageManager.ViewTaskPage.ClickEditButton();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.ClearTheData();
			Main.PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			Main.PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
			Main.PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void DeleteTask()
		{
			//Act
			Main.PageManager.ViewTaskPage.NavigateToViewPage(taskId);
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.ViewTaskPage.ClickEditButton();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.ClickDeleteButton();

			//Assert
			Assert.Throws<NoSuchElementException>(() => Main.PageManager.TaskPage.SearchByTaskTitle(taskTitle));
		}

		[Test]
		[Ignore ("can be run only with selenoid started excluded from common tuns")]
		public void GoogleWithSelenoidRun()
		{
			var URL = "https://www.google.com/";

			var capabilities = new DesiredCapabilities();
			capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
			capabilities.SetCapability(CapabilityType.BrowserVersion, "84.0");
			capabilities.SetCapability("enableVNC", true);
			var remoteDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), capabilities);

			remoteDriver.Navigate().GoToUrl(URL);

			//Assert
			Assert.AreEqual(URL, remoteDriver.Url);
		}

		[TearDown]
		public void TearDown()
		{
			Main.Clean();
		}
	}
}
