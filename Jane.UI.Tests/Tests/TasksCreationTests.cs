using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TasksCreationTests
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			//Arrange
			var loginPage = new LoginPage();
			driver = loginPage.Driver;

			//Act
			loginPage.NavigateAndLogin();
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Arrange
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
			using var addTaskPage = new AddEditTaskPage(driver);

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
	}
}
