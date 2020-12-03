using Jane.Tests.Infrastructure;
using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageAuthorizedTests
	{
		PageManager pageManager;

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
			pageManager.TaskPage.ClickAboutLink();
			pageManager.SwitchTab(1);

			//Assert
			pageManager.AboutPage.EnsurePageLoaded();
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
		}
		[TearDown]
		public void TearDown()
		{
			pageManager.TaskPage.CreateScreenshotIfFailed();
			pageManager.CleanWebDriver();
		}
	}
}
