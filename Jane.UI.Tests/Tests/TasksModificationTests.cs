using System;
using System.Threading.Tasks;
using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using Jane.UI.Tests.Infrastructure;
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
		private PageManager PageManager;

		[OneTimeSetUp]
		public void Autorize()
		{
			var auth = new Authentication();
			token = auth.LogIn();
		}

		[SetUp]
		public async Task Setup()
		{
			PageManager = new PageManager();
			//navigate to login page
			PageManager.LoginPage.NavigateAndLogin();

			var client = new RestClient(Config.Instance["appSettings:apiURL"]);
			client.Authenticator = new JwtAuthenticator(token);
			var request = new RestRequest("/api/todo", Method.POST, DataFormat.Json);
			request.AddJsonBody(new TodoTaskDto
			{
				Created = DateTimeOffset.UtcNow,
				DueDate = Randoms.GenerateRandomDate(),
				Note = Randoms.GenerateStringValueInRange(5, 250),
				Title = Randoms.GenerateStringValueInRange(1, 250)
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
			PageManager.ViewTaskPage.NavigateToViewPage(taskId);
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();
			PageManager.ViewTaskPage.ClickEditButton();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.ClearTheData();
			PageManager.AddEditTaskPage.CheckFinishedCheckbox();
			PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
			PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void OpenEditTaskFromTasksPage()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.ClickEditLatestCreatedTask();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.AllItemsArePresented());
		}

		[Test]
		public void OpenInfoTaskFromTasksPage()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.ClickInfoForLatestCreatedTask();

			//Assert
			PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
		}

		[Test]
		public void EditTaskFromInfoPage()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.ClickInfoForLatestCreatedTask();
			PageManager.ViewTaskPage.ClickEditButton();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.ClearTheData();
			PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
			PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void DeleteTask()
		{
			//Act
			PageManager.ViewTaskPage.NavigateToViewPage(taskId);
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();
			PageManager.ViewTaskPage.ClickEditButton();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.ClickDeleteButton();

			//Assert
			Assert.Throws<NoSuchElementException>(() => PageManager.TaskPage.SearchByTaskTitle(taskTitle));
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
			PageManager.CleanWebDriver();
		}
	}
}
