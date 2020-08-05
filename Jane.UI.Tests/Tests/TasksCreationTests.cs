using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TasksCreationTests
	{
		private IWebDriver driver;
		private IConfigurationRoot configuration;

		[SetUp]
		public void Setup()
		{
			//Arrange
			ChromeOptions options = new ChromeOptions();
			options.PageLoadStrategy = PageLoadStrategy.Normal;
			driver = new ChromeDriver(options);
			var loginPage = new LoginPage(driver);

			//Act
			loginPage.NavigateAndLogin();
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

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
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var currentDate = addTaskPage.CurrentDate();
			var duedate = addTaskPage.ReturnDueDateDefaultValue();

			//Assert
			Assert.That(currentDate, Is.EqualTo(duedate));
		}

		[Test]
		public void SubmitEmptyFields()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.EmptyFormsValidationCheck());
		}

		[Test]
		public void SubmitEmptyTitle()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.EmptyTitleValidationCheck());
		}
		[Test]
		public void SubmitEmptyTaskNote()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.EmptyBodyValidationCheck());
		}
		[Test]
		public void SubmitValueBiggerThanAllowedToTitle()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			addTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.TitleMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitValueBiggerThanAllowedToNote()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			addTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.BodyMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitAllValuesBiggerThanAllowed()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			addTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.AllFieldsMoreThan250ValidationCheck());
		}
		
		[Test]
		public void SubmitValuessToNoteSmallerThanAllowed()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			addTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 4));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.BodyLessThan5ItemsCheck());
		}

		[Test]
		public void SubmitDataWithDefaultDateToTasks()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var viewTask = addTaskPage.PopulateAllItemsAndSubmit(false);
			viewTask.WaitForPageToBeLoaded();

			//Assert
			viewTask.EnsurePageLoaded();
			Assert.IsTrue(viewTask.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(addTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void SubmitAllCorrectDataToTasks()
		{
			//Arrange
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var viewTask = addTaskPage.PopulateAllItemsAndSubmit();
			viewTask.WaitForPageToBeLoaded();
			

			//Assert
			viewTask.EnsurePageLoaded();
			Assert.IsTrue(viewTask.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(addTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void TaskPresenceInTheList()
		{
			//Arrange 
			var addTaskPage = new AddEditTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var viewPage = addTaskPage.PopulateAllItemsAndSubmit();
			viewPage.WaitForPageToBeLoaded();
			var title = addTaskPage.ReturnTitleText();
			var tasksPage = new TaskPage(driver);
			tasksPage.NavigateTo();
			tasksPage.WaitForPageLoaded();
			var allTasks = tasksPage.ListOfTasks();

			//Assert
			Assert.That(allTasks, Contains.Item(title));
		}

		[TearDown]
		public void Teardown()
		{
			driver.Dispose();
		}
	}
}
