using Jane.Tests.Infrastructure;
using Jane.UI.Tests.PageObjectModels;
using Jane.UI.Tests.TestServices;
using NUnit.Framework;

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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
			Assert.IsTrue(pageManager.AddEditTaskPage.AllItemsArePresented());
		}

		[Test]
		public void DateIsDisplayedCorrectly()
		{
			//Act
			pageManager.AddEditTaskPage.NavigateTo();
			pageManager.AddEditTaskPage.WaitForPageToBeLoaded();
			var currentDate = pageManager.AddEditTaskPage.CurrentDate();
			var duedate = pageManager.AddEditTaskPage.ReturnDueDateDefaultValue();
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.AddEditTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
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
			pageManager.ViewTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

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
			pageManager.ViewTaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

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
			pageManager.TaskPage.CreateScreenshot(TestContext.CurrentContext.Test.Name);

			//Assert
			Assert.That(allTasks, Contains.Item(title));
		}

		[TearDown]
		public void TearDown()
		{
			pageManager.CleanWebDriver();
		}
	}
}
