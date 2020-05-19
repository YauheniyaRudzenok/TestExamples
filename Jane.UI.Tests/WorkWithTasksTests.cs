using System;
using System.Collections.Generic;
using System.Text;
using Jane.UI.Tests.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{

	public class WorkWithTasksTests
	{
		private const string Name = "Jane";
		private const string Password = "Password";

		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			//Arrange
			driver = new ChromeDriver();
			LoginPage loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateAndLogin(Name, Password);
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Arrange
			AddTaskPage addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.AllItemsArePresented());
		}

		[Test]
		public void DateIsDisplayedCorrectly()
		{
			//Arrange
			AddTaskPage addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var currentDate = DateTime.Today.ToString("yyyy-MM-dd");
			var duedate = addTaskPage.DueDateDefaultValue();

			//Assert
			Assert.That(currentDate, Is.EqualTo(duedate));
		}

		[Test]
		public void SubmitEmptyFields()
		{
			//Arrange
			AddTaskPage addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.ClickSave();

			//Assert
			Assert.IsTrue(addTaskPage.ValidationIsApplied());
		}

		[TearDown]
		public void Teardown()
		{
			driver.Dispose();
		}
	}
}
