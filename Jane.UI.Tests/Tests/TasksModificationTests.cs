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
using Jane.Tests.Infrastructure;
using OpenQA.Selenium.Remote;
using Microsoft.Extensions.Configuration;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	class TasksModificationTests
	{
		private IWebDriver driver;
		private string token;
		private int taskId;
		private string taskTitle;
		private IConfigurationRoot configuration; 

		[OneTimeSetUp]
		public void Autorize()
		{

			var auth = new Authentication();
			token = auth.LogIn();
			configuration = Config.Instance;
			driver = BrowserFabric.CreateDriver(configuration["browserSettings:browser"]);
			var loginPage = new LoginPage(driver);

			loginPage.NavigateAndLogin();

		}

		[SetUp]
		public async Task Setup()
		{
			taskTitle = Randoms.GenerateStringValueInRange(1, 250);

			var client = new RestClient(configuration["appSettings:apiURL"]);
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
			//Arrange
			var viewTask = new ViewTaskPage(driver);

			//Act
			viewTask.NavigateToViewPage(taskId);
			viewTask.WaitForPageToBeLoaded();
			var editPage = viewTask.ClickEditButton();
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

		[Test]
		public void OpenEditTaskFromTasksPage()
		{
			//Arrange
			var tasksPage = new TaskPage(driver);

			//Act
			tasksPage.NavigateTo();
			tasksPage.WaitForPageLoaded();
			var editPage = tasksPage.ClickEditLatestCreatedTask();
			editPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(editPage.AllItemsArePresented());
		}

		[Test]
		public void OpenInfoTaskFromTasksPage()
		{
			//Arrange
			var tasksPage = new TaskPage(driver);

			//Act
			tasksPage.NavigateTo();
			tasksPage.WaitForPageLoaded();
			var viewTaskPage = tasksPage.ClickInfoForLatestCreatedTask();

			//Assert
			viewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(viewTaskPage.CheckFinishedStatusIsCorrect());
		}

		[Test]
		public void EditTaskFromInfoPage()
		{
			//Arrange
			var tasksPage = new TaskPage(driver);

			//Act
			tasksPage.NavigateTo();
			tasksPage.WaitForPageLoaded();
			var viewTask = tasksPage.ClickInfoForLatestCreatedTask();
			var editPage = viewTask.ClickEditButton();
			editPage.WaitForPageToBeLoaded();
			editPage.ClearTheData();
			editPage.PopulateAllItemsAndSubmit();
			viewTask.WaitForPageToBeLoaded();

			//Assert
			viewTask.EnsurePageLoaded();
			Assert.IsTrue(editPage.EnsureAllItemsAreSavedCorrectly());
			viewTask.CheckFinishedStatusIsCorrect(false);
		}

		[Test]
		public void DeleteTask()
		{
			//Arrange
			var viewTask = new ViewTaskPage(driver);

			//Act
			viewTask.NavigateToViewPage(taskId);
			viewTask.WaitForPageToBeLoaded();
			var editPage = viewTask.ClickEditButton();
			editPage.WaitForPageToBeLoaded();
			editPage.ClickDeleteButton();
			var tasksPage = new TaskPage(driver);

			//Assert
			Assert.Throws<NoSuchElementException>(() => tasksPage.SearchByTaskTitle(taskTitle));
		}

		[Test]
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

		[OneTimeTearDown]
		public void TearDown()
		{
			driver.Dispose();
		}

	}
}
