using System;
using System.Collections.Generic;
using System.Text;
using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	public class TaskPageAuthorized
	{
		private IWebDriver driver;
		private const string name = "Jane";
		private const string password = "Password";

		[OneTimeSetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			var loginPage = new LoginPage(driver);

			loginPage.NavigateAndLogin(name, password);
		}

		[Test]
		public void Navigate()
		{
			//Arrange
			var taskPage = new TaskPage(driver);

			//Act
			taskPage.NavigateTo();
			taskPage.WaitForPageLoaded();

			//Assert
			taskPage.EnsureAllItemsAreDisplayed();
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			driver.Dispose();
		}
	}
}
