using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TasksCreationTests
	{
		private PageManager PageManager;

		[SetUp]
		public void Setup()
		{
			//Act
			PageManager = new PageManager();
			PageManager.LoginPage.NavigateAndLogin();
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.AllItemsArePresented());
		}

		[Test]
		public void DateIsDisplayedCorrectly()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			var currentDate = PageManager.AddEditTaskPage.CurrentDate();
			var duedate = PageManager.AddEditTaskPage.ReturnDueDateDefaultValue();

			//Assert
			Assert.That(currentDate, Is.EqualTo(duedate));
		}

		[Test]
		public void SubmitEmptyFields()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.EmptyFormsValidationCheck());
		}

		[Test]
		public void SubmitEmptyTitle()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.EmptyTitleValidationCheck());
		}
		[Test]
		public void SubmitEmptyTaskNote()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.EmptyBodyValidationCheck());
		}
		[Test]
		public void SubmitValueBiggerThanAllowedToTitle()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.TitleMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitValueBiggerThanAllowedToNote()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.BodyMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitAllValuesBiggerThanAllowed()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.AllFieldsMoreThan250ValidationCheck());
		}
		
		[Test]
		public void SubmitValuessToNoteSmallerThanAllowed()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 4));
			PageManager.AddEditTaskPage.ClickSave();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(PageManager.AddEditTaskPage.BodyLessThan5ItemsCheck());
		}

		[Test]
		public void SubmitDataWithDefaultDateToTasks()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit(false);
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void SubmitAllCorrectDataToTasks()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();


			//Assert
			PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void TaskPresenceInTheList()
		{
			//Act
			PageManager.AddEditTaskPage.NavigateTo();
			PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			PageManager.ViewTaskPage.WaitForPageToBeLoaded();
			var title = PageManager.AddEditTaskPage.ReturnTitleText();
			PageManager.TaskPage.NavigateTo();
			PageManager.TaskPage.WaitForPageLoaded();
			var allTasks = PageManager.TaskPage.ListOfTasks();

			//Assert
			Assert.That(allTasks, Contains.Item(title));
		}

		[TearDown]
		public void TearDown()
		{
			PageManager.CleanWebDriver();
		}
	}
}
