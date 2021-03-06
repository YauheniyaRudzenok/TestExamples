﻿using Jane.Tests.Infrastructure;
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
		public void CreatePageManager()
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

			pageManager.TaskPage.ClickAboutLink();
			pageManager.SwitchTab(1);
			pageManager.AboutPage.WaitAboutPageToBeLoaded();

			//Assert
			pageManager.AboutPage.EnsurePageLoaded();
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
			pageManager.TaskPage.CreateScreenshotIfFailed();
			pageManager.CleanWebDriver();
		}
	}
}
