using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageAuthorizedTests
	{
		PageManager PageManager;

		[SetUp]
		public void Setup()
		{
			PageManager = new PageManager();
			PageManager.LoginPage.NavigateAndLogin();
		}

		[Test]
		public void ShouldContainAllElements()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(PageManager.TaskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(PageManager.TaskPage.EnsureAllMenuItemsAreDisplayed(true));
			Assert.IsTrue(PageManager.TaskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.ClickAboutLink();
			PageManager.SwitchTab(1);

			//Assert
			PageManager.AboutPage.EnsurePageLoaded();
		}

		[Test]
		public void NavigateToAddTaskPage()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.NavigateToAddTask();

			//Assert
			PageManager.AddEditTaskPage.EnsurePageLoaded();
		}

		[Test]
		public void LogOut()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.ClickLogoutButton();
			PageManager.TaskPage.WaitForPageLoaded();

			//Assert
			PageManager.TaskPage.EnsurePageLoaded();
			Assert.IsTrue(PageManager.TaskPage.EnsureAllMenuItemsAreDisplayed());
		}
		[TearDown]
		public void TearDown()
		{
			PageManager.CleanWebDriver();
		}
	}
}
