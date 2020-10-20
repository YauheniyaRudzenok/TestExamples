using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	class TaskPageDefaultTests
	{
		[Test]
		public void ShouldContainAllElements()
		{
			//Arrange
			using var taskPage = new TaskPage();
			
			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			//Assert
			Assert.IsTrue(taskPage.EnsureAllHeaderItemsAreDisplayed());
			Assert.IsTrue(taskPage.EnsureAllMenuItemsAreDisplayed());
			Assert.IsTrue(taskPage.ReturnAboutPageLinkText());
		}

		[Test]
		public void AboutLinkNavigation()
		{
			//Arrange
			using var taskPage = new TaskPage();

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			AboutPage aboutPage = taskPage.ClickAboutLink();
			aboutPage.Driver.SwitchTo().Window(aboutPage.Driver.WindowHandles[1]);

			//Assert
			aboutPage.EnsurePageLoaded();
		}

		[Test]
		public void SignInLinkNavigation()
		{
			//Arrange
			using var taskPage = new TaskPage();

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();
			var loginPage=taskPage.NavigateToLogin();

			//Assert
			loginPage.EnsurePageLoaded();
		}
	}
}
