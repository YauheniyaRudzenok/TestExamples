using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageAuthorizedTests
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			var loginPage = new LoginPage();

			loginPage.NavigateAndLogin();
			driver = loginPage.Driver;
		}

		[Test]
		public void ShouldContainAllElements()
		{
			//Arrange
			using var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(taskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed(true));
			Assert.IsTrue(taskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Arrange
			using var taskPage = new TaskPage(driver);

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
			using var taskPage = new TaskPage(driver);

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
			using var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();
			taskPage.ClickLogoutButton();
			taskPage.WaitForPageLoaded();

			//Assert
			taskPage.EnsurePageLoaded();
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed());
		}
	}
}
