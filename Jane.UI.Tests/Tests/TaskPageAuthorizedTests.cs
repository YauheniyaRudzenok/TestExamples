using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageAuthorizedTests
	{
		[SetUp]
		public void Setup()
		{
			Main.PageManager.LoginPage.NavigateAndLogin();
		}

		[Test]
		public void ShouldContainAllElements()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.TaskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(Main.PageManager.TaskPage.EnsureAllMenuItemsAreDisplayed(true));
			Assert.IsTrue(Main.PageManager.TaskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.ClickAboutLink();
			Main.PageManager.SwitchTab(1);

			//Assert
			Main.PageManager.AboutPage.EnsurePageLoaded();
		}

		[Test]
		public void NavigateToAddTaskPage()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.NavigateToAddTask();

			//Assert
			Main.PageManager.AddEditTaskPage.EnsurePageLoaded();
		}

		[Test]
		public void LogOut()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.ClickLogoutButton();
			Main.PageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Main.PageManager.TaskPage.EnsurePageLoaded();
			Assert.IsTrue(Main.PageManager.TaskPage.EnsureAllMenuItemsAreDisplayed());
		}
		[TearDown]
		public void TearDown()
		{
			Main.Clean();
		}
	}
}
