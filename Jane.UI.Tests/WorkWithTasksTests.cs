using System;
using System.Collections.Generic;
using System.Text;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
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
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateAndLogin(Name, Password);
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

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
			var addTaskPage = new AddTaskPage(driver);

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
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.emptyFormsValidationCheck());
		}

		[Test]
		public void SubmitEmptyTitle()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.TaskBody().SendKeys(TestService.GenerateStringValueInRange(5, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.emptyTitleValidationCheck());
		}
		[Test]
		public void SubmitEmptyTaskNote()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(TestService.GenerateStringValueInRange(5, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.emptyBodyValidationCheck());
		}
		[Test]
		public void SubmitValueBiggerThanAllowedToTitle()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(TestService.GenerateStringValueInRange(251, 400));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.titleMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitValueBiggerThanAllowedToNote()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.TaskBody().SendKeys(TestService.GenerateStringValueInRange(251, 400));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.bodyMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitAllValuesBiggerThanAllowed()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(TestService.GenerateStringValueInRange(251, 400));
			addTaskPage.TaskBody().SendKeys(TestService.GenerateStringValueInRange(251, 400));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.allFieldsMoreThan250ValidationCheck());
		}

		[TearDown]
		public void Teardown()
		{
			driver.Dispose();
		}
	}
}
