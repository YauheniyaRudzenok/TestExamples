using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TaskPageDefaultTests
	{
		private PageManager pageManager;

		[SetUp]
		public void SetUp()
		{
			pageManager = new PageManager();
		}

		[Test]
		public void ShouldContainAllElements()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(pageManager.TaskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(pageManager.TaskPage.EnsureAllMenuItemsAreDisplayed());
			Assert.IsTrue(pageManager.TaskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();

			AboutPage aboutPage = pageManager.TaskPage.ClickAboutLink();
			aboutPage.Driver.SwitchTo().Window(aboutPage.Driver.WindowHandles[1]);

			//Assert
			aboutPage.EnsurePageLoaded();
		}

		[Test]
		public void SignInLinkNavigation()
		{
			//Act
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			pageManager.TaskPage.NavigateToLogin();

			//Assert
			pageManager.LoginPage.EnsurePageLoaded();
		}
		[TearDown]
		public void TearDown()
		{
			pageManager.Clean();
		}
	}
}
