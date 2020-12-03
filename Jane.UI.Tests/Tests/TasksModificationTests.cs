using System;
using System.Threading.Tasks;
using Jane.Tests.Infrastructure;
using Jane.Todo.Dto;
using Jane.UI.Tests.Infrastructure;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
		private PageManager pageManager;

		[OneTimeSetUp]
		public void Autorize()
		{
			var auth = new Authentication();
			token = auth.LogIn();
		}

		[SetUp]
		public async Task Setup()
		{
			pageManager = new PageManager();
			//navigate to login page
			pageManager.LoginPage.NavigateAndLogin();

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
			pageManager.ViewTaskPage.NavigateToViewPage(taskId);
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();
			pageManager.ViewTaskPage.ClickEditButton();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.ClearTheData();
			pageManager.AddEditTaskPage.CheckFinishedCheckbox();
			pageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
			pageManager.ViewTaskPage.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void OpenEditTaskFromTasksPage()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			pageManager.TaskPage.ClickEditLatestCreatedTask();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(pageManager.AddEditTaskPage.AllItemsArePresented());
		}

		[Test]
		public void OpenInfoTaskFromTasksPage()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			pageManager.TaskPage.ClickInfoForLatestCreatedTask();

			//Assert
			pageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
		}

		[Test]
		public void EditTaskFromInfoPage()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			pageManager.TaskPage.ClickInfoForLatestCreatedTask();
			pageManager.ViewTaskPage.ClickEditButton();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.ClearTheData();
			pageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
			pageManager.ViewTaskPage.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void DeleteTask()
		{
			//Act
			pageManager.ViewTaskPage.NavigateToViewPage(taskId);
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();
			pageManager.ViewTaskPage.ClickEditButton();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.ClickDeleteButton();

			//Assert
			Assert.Throws<NoSuchElementException>(() => pageManager.TaskPage.SearchByTaskTitle(taskTitle));
		}

		[Test]
		[Ignore ("can be run only with selenoid started excluded from common runs")]
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
			pageManager.AddEditTaskPage.CreateScreenshotIfFailed();
			pageManager.CleanWebDriver();
		}
	}
}
