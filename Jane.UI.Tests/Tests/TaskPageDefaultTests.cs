using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageDefaultTests
	{
		private PageManager PageManager;
		[SetUp]
		public void CreatePageManager()
        {
			PageManager = new PageManager();
        }

		[Test]
		public void ShouldContainAllElements()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(PageManager.TaskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(PageManager.TaskPage.EnsureAllMenuItemsAreDisplayed());
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
		public void SignInLinkNavigation()
		{
			//Act
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			PageManager.TaskPage.NavigateToLogin();

			//Assert
			PageManager.LoginPage.EnsurePageLoaded();
		}
		[TearDown]
		public void TearDown()
		{
			PageManager.CleanWebDriver();
		}
	}
}
