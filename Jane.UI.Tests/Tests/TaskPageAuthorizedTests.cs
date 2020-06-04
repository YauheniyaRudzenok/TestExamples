using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	public class TaskPageAuthorizedTests
	{

		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			loginPage.NavigateAndLogin();
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
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed(true));
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
			AddEditTaskPage addTaskPage = taskPage.NavigateToAddTask();

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
			var failurePage = new LoginFailedPage(driver);
			failurePage.WaitForPageLoaded();

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
