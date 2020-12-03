using Jane.Tests.Infrastructure;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TasksCreationTests
	{
		private PageManager pageManager;

		[SetUp]
		public void Setup()
		{
			//Act
			pageManager = new PageManager();
			pageManager.LoginPage.NavigateAndLogin();
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(pageManager.AddEditTaskPage.AllItemsArePresented());
			pageManager.AddEditTaskPage.EnsurePageLoaded();
		}

		[Test]
		public void DateIsDisplayedCorrectly()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			var currentDate = pageManager.AddEditTaskPage.CurrentDate();
			var duedate = pageManager.AddEditTaskPage.ReturnDueDateDefaultValue();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.That(currentDate, Is.EqualTo(duedate));
		}

		[Test]
		public void SubmitEmptyFields()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.EmptyFormsValidationCheck());
		}

		[Test]
		public void SubmitEmptyTitle()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.EmptyTitleValidationCheck());
		}
		[Test]
		public void SubmitEmptyTaskNote()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.EmptyBodyValidationCheck());
		}
		[Test]
		public void SubmitValueBiggerThanAllowedToTitle()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			pageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.TitleMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitValueBiggerThanAllowedToNote()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			pageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.BodyMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitAllValuesBiggerThanAllowed()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			pageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.AllFieldsMoreThan250ValidationCheck());
		}
		
		[Test]
		public void SubmitValuessToNoteSmallerThanAllowed()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			pageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 4));
			pageManager.AddEditTaskPage.ClickSave();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.AddEditTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.AddEditTaskPage.BodyLessThan5ItemsCheck());
		}

		[Test]
		public void SubmitDataWithDefaultDateToTasks()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.PopulateAllItemsAndSubmit(false);
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(pageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void SubmitAllCorrectDataToTasks()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			pageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(pageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(pageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void TaskPresenceInTheList()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			pageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			pageManager.ViewTaskPage.WaitForPageToBeLoaded();
			var title = pageManager.AddEditTaskPage.ReturnTitleText();
			pageManager.TaskPage.NavigateTo();
			pageManager.TaskPage.WaitForPageLoaded();
			var allTasks = pageManager.TaskPage.ListOfTasks();

			//Assert
			Assert.That(allTasks, Contains.Item(title));
		}

		[TearDown]
		public void TearDown()
		{

			pageManager.ViewTaskPage.CreateScreenshotIfFailed();
			pageManager.CleanWebDriver();
		}
	}
}
