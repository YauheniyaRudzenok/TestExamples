using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageDefaultTests
	{
		[Test]
		public void ShouldContainAllElements()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.TaskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(Main.PageManager.TaskPage.EnsureAllMenuItemsAreDisplayed());
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
		public void SignInLinkNavigation()
		{
			//Act
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			Main.PageManager.TaskPage.NavigateToLogin();

			//Assert
			Main.PageManager.LoginPage.EnsurePageLoaded();
		}
		[TearDown]
		public void TearDown()
		{
			Main.Clean();
		}
	}
}
