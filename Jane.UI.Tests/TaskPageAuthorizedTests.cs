using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	public class TaskPageAuthorizedTests
	{
		//move to config in the future
		private const string Name = "Jane";
		private const string Password = "Password";

		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			loginPage.NavigateAndLogin(Name, Password);
		}

		[Test]
		public void ShouldContainAllElements()
		{
			//Arrange
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(taskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed(true, Name));
			Assert.IsTrue(taskPage.AboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Arrange
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();
			AboutPage aboutPage = taskPage.ClickAboutLink();
			driver.SwitchTo().Window(driver.WindowHandles[1]);

			//Assert
			aboutPage.EnsurePageLoaded();
		}

		[Test]
		public void NavigateToAddTaskPage()
		{
			//Arrange
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();
			AddTaskPage addTaskPage = taskPage.NavigateToAddTask();

			//Assert
			addTaskPage.EnsurePageLoaded();
		}

		[Test]
		public void LogOut()
		{
			//Arrange
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();
			taskPage.ClickLogoutButton();
			taskPage.WaitForPageLoaded();

			//Assert
			taskPage.EnsurePageLoaded();
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed());
		}

		[TearDown]
		public void TearDown()
		{
			driver.Dispose();
		}
	}
}
