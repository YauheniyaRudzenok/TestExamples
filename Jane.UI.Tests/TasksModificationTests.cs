using System;
using System.Collections.Generic;
using System.Text;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Jane.UI.Tests
{
	class TasksModificationTests
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			/// Performance can be imroved via using API instead of UI steps in SetUp
			
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var titleText = TestService.GenerateStringValueInRange(1, 250);
			addTaskPage.Title().SendKeys(titleText);
			var bodyText = (TestService.GenerateStringValueInRange(5, 250));
			addTaskPage.TaskBody().SendKeys(bodyText);
			var date = TestService.GenerateRandomDateToString();
			addTaskPage.DueDateDefaultValueReplace(date);
			addTaskPage.ClickSave();
			var viewTask = new ViewTaskPage(driver);
			viewTask.WaitForPageToBeLoaded();
		}


	}
}
