using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageAuthorizedTests
	{
		private PageManager pageManager;

		[SetUp]
		public void Setup()
		{
			pageManager = new PageManager();
			pageManager.LoginPage.NavigateAndLogin();
		}

		[Test]
		public void ShouldContainAllElements()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(pageManager.TaskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(pageManager.TaskPage.EnsureAllMenuItemsAreDisplayed(true));
			Assert.IsTrue(pageManager.TaskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			AboutPage aboutPage = pageManager.TaskPage.ClickAboutLink();
			var driver = pageManager.TaskPage.Driver;
			driver.SwitchTo().Window(driver.WindowHandles[1]);

			//Assert
			aboutPage.EnsurePageLoaded();
		}

		[Test]
		public void NavigateToAddTaskPage()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			pageManager.TaskPage.NavigateToAddTask();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
		}

		[Test]
		public void LogOut()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			pageManager.TaskPage.ClickLogoutButton();
			pageManager.TaskPage.WaitForPageLoaded();

			//Assert
			pageManager.TaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.TaskPage.EnsureAllMenuItemsAreDisplayed());
		}
		[TearDown]
		public void TearDown()
		{
			pageManager.Clean();
		}
	}
}
