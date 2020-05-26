using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Jane.UI.Tests
{

	public class TasksCreationTests
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
			var currentDate = addTaskPage.CurrentDate();
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
			Assert.IsTrue(addTaskPage.EmptyFormsValidationCheck());
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
			Assert.IsTrue(addTaskPage.EmptyTitleValidationCheck());
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
			Assert.IsTrue(addTaskPage.EmptyBodyValidationCheck());
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
			addTaskPage.TaskBody().SendKeys(TestService.GenerateStringValueInRange(1, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.TitleMoreThan250ValidationCheck());
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
			addTaskPage.Title().SendKeys(TestService.GenerateStringValueInRange(1, 250));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.BodyMoreThan250ValidationCheck());
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
			Assert.IsTrue(addTaskPage.AllFieldsMoreThan250ValidationCheck());
		}
		
		[Test]
		public void SubmitValuessToNoteSmallerThanAllowed()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			addTaskPage.Title().SendKeys(TestService.GenerateStringValueInRange(1, 250));
			addTaskPage.TaskBody().SendKeys(TestService.GenerateStringValueInRange(1, 4));
			addTaskPage.ClickSave();
			addTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(addTaskPage.BodyLessThan5ItemsCheck());
		}

		[Test]
		public void SubmitDataWithDefaultDateToTasks()
		{
			//Arrange
			var addTaskPage = new AddTaskPage(driver);

			//Act
			addTaskPage.NavigateTo();
			addTaskPage.WaitForPageToBeLoaded();
			var titleText = TestService.GenerateStringValueInRange(1, 250);
			addTaskPage.Title().SendKeys(titleText);
			var bodyText = TestService.GenerateStringValueInRange(5, 250);
			addTaskPage.TaskBody().SendKeys(bodyText);
			addTaskPage.ClickSave();
			var viewTask = new ViewTaskPage(driver);
			viewTask.WaitForPageToBeLoaded();

			//Assert
			viewTask.EnsurePageLoaded();
			Assert.That(viewTask.TaskTitleText(), Is.EqualTo(titleText));
			Assert.IsTrue(viewTask.CheckFinishedStatusIsCorrect());
			Assert.That(viewTask.TaskItems()[1], Is.EqualTo(bodyText));
			Assert.That(viewTask.StringCreationDateValue(), Is.EqualTo(addTaskPage.CurrentDate()));
			Assert.That(viewTask.StringDueDateValue(), Is.EqualTo(addTaskPage.CurrentDate()));
		}

		[Test]
		public void SubmitAllCorrectDataToTasks()
		{
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
			

			//Assert
			viewTask.EnsurePageLoaded();
			Assert.That(viewTask.TaskTitleText(), Is.EqualTo(titleText));
			Assert.IsTrue(viewTask.CheckFinishedStatusIsCorrect());
			Assert.That(viewTask.TaskItems()[1], Is.EqualTo(bodyText));
			Assert.That(viewTask.StringCreationDateValue(), Is.EqualTo(addTaskPage.CurrentDate()));
			Assert.That(viewTask.StringDueDateValue(), Is.EqualTo(date));
		}

		[TearDown]
		public void Teardown()
		{
			driver.Dispose();
		}
	}
}
