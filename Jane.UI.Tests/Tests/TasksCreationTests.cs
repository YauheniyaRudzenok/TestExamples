using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;

namespace Jane.UI.Tests
{
	[TestFixture]
	[Parallelizable]
	public class TasksCreationTests
	{

		[SetUp]
		public void Setup()
		{
			//Act
			Main.PageManager.LoginPage.NavigateAndLogin();
		}

		[Test]
		public void AllItemsAreDisplayed()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.AllItemsArePresented());
		}

		[Test]
		public void DateIsDisplayedCorrectly()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			var currentDate = Main.PageManager.AddEditTaskPage.CurrentDate();
			var duedate = Main.PageManager.AddEditTaskPage.ReturnDueDateDefaultValue();

			//Assert
			Assert.That(currentDate, Is.EqualTo(duedate));
		}

		[Test]
		public void SubmitEmptyFields()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EmptyFormsValidationCheck());
		}

		[Test]
		public void SubmitEmptyTitle()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EmptyTitleValidationCheck());
		}
		[Test]
		public void SubmitEmptyTaskNote()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(5, 250));
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EmptyBodyValidationCheck());
		}
		[Test]
		public void SubmitValueBiggerThanAllowedToTitle()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			Main.PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.TitleMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitValueBiggerThanAllowedToNote()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			Main.PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.BodyMoreThan250ValidationCheck());
		}

		[Test]
		public void SubmitAllValuesBiggerThanAllowed()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			Main.PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(251, 400));
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.AllFieldsMoreThan250ValidationCheck());
		}
		
		[Test]
		public void SubmitValuessToNoteSmallerThanAllowed()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.Title().SendKeys(Randoms.GenerateStringValueInRange(1, 250));
			Main.PageManager.AddEditTaskPage.TaskBody().SendKeys(Randoms.GenerateStringValueInRange(1, 4));
			Main.PageManager.AddEditTaskPage.ClickSave();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();

			//Assert
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.BodyLessThan5ItemsCheck());
		}

		[Test]
		public void SubmitDataWithDefaultDateToTasks()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit(false);
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();

			//Assert
			Main.PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(Main.PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void SubmitAllCorrectDataToTasks()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();


			//Assert
			Main.PageManager.ViewTaskPage.EnsurePageLoaded();
			Assert.IsTrue(Main.PageManager.ViewTaskPage.CheckFinishedStatusIsCorrect());
			Assert.IsTrue(Main.PageManager.AddEditTaskPage.EnsureAllItemsAreSavedCorrectly());
		}

		[Test]
		public void TaskPresenceInTheList()
		{
			//Act
			Main.PageManager.AddEditTaskPage.NavigateTo();
			Main.PageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			Main.PageManager.AddEditTaskPage.PopulateAllItemsAndSubmit();
			Main.PageManager.ViewTaskPage.WaitForPageToBeLoaded();
			var title = Main.PageManager.AddEditTaskPage.ReturnTitleText();
			Main.PageManager.TaskPage.NavigateTo();
			Main.PageManager.TaskPage.WaitForPageLoaded();
			var allTasks = Main.PageManager.TaskPage.ListOfTasks();

			//Assert
			Assert.That(allTasks, Contains.Item(title));
		}

		[TearDown]
		public void TearDown()
		{
			Main.Clean();
		}
	}
}
