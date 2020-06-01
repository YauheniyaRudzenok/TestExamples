using System;
using System.Collections.Generic;
using Jane.UI.Tests.TestServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public class AddEditTaskPage:Page
	{
		#region Constants

		private const string TaskTitleLabel = "Title:";
		private const string TaskBodyLabel = "Note:";
		private const string TaskDueDateLabel = "Due date:";
		private const string TitlePlaceholderText = "Enter title";
		private const string TaskPlaceholder = "Enter note";
		private const string SaveButtonText = "Save";
		private const string TitleValidationMessage = "The Title field is required.";
		private const string TaskBodyValidationMessage = "The Note field is required.";
		private const string TitleMoreThan250ValidationMessage = "The field Title must be a string or array type with a maximum length of '250'.";
		private const string BodyMoreThan250ValidationMessage = "The field Note must be a string or array type with a maximum length of '250'.";
		private const string BodyLessThan5ItemsValidationMessage = "The field Note must be a string or array type with a minimum length of '5'.";

		#endregion
		#region Constructors
		public AddEditTaskPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "http://localhost:63508/taskedit";
		#endregion

		#region Variables

		private string titleText;
		private string bodyText;
		private string date;

		#endregion

		#region Elements
		public IWebElement SaveElement() => Driver.FindElement(By.CssSelector("button[type='submit']"));
		public IWebElement DueDateElement() => Driver.FindElement(By.CssSelector("input[id='dueDate']"));
		public IWebElement Title() => Driver.FindElement(By.CssSelector("input[id='title']"));
		public IWebElement TaskBody() => Driver.FindElement(By.CssSelector("textarea[id='note'"));
		#endregion

		#region Actions
		public string TitleLabelText() => Driver.FindElement(By.CssSelector("label[for='title']")).Text;

		public string TaskText() => Driver.FindElement(By.CssSelector("label[for='note']")).Text;

		public string DateText() => Driver.FindElement(By.CssSelector("label[for='dueDate']")).Text;

		public string TitlePlaceholder()=> Driver.FindElement(By.CssSelector("input[id='title']")).GetAttribute("placeholder");

		public string BodyPlaceholder() => Driver.FindElement(By.CssSelector("textarea[id='note'")).GetAttribute("placeholder");

		public void WaitForPageToBeLoaded()
		{
			WebDriverWait wait = new WebDriverWait(Driver, timeout:TimeSpan.FromSeconds(10));
			wait.Until(ExpectedConditions.ElementIsVisible((By.CssSelector("button[type='submit']"))));
		}
		public string SaveButtonItemText() => SaveElement().Text;

		public string DueDateDefaultValue() => DueDateElement().GetAttribute("value");
		public bool AllItemsArePresented()
		{
			bool allItemsAreDisplayed = TitleLabelText() == TaskTitleLabel &&
										TaskText() == TaskBodyLabel &&
										DateText() == TaskDueDateLabel &&
										TitlePlaceholder() == TitlePlaceholderText &&
										BodyPlaceholder() == TaskPlaceholder &&
										SaveButtonItemText() == SaveButtonText;
			return allItemsAreDisplayed;
		}
		public void ClickSave() => SaveElement().Click();

		public List<string> ValidationCheck()
		{
			var messagesElements=Driver.FindElements(By.CssSelector("div[class^='validation']"));
			var messages = new List<string>();
			foreach(IWebElement item in messagesElements)
			{
				messages.Add(item.Text);
			}
			return messages;
		}

		public bool EmptyTitleValidationCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(TitleValidationMessage) &&
											validation.Count == 1;
			return validationCheckIsPassed;
		}

		public bool EmptyBodyValidationCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(TaskBodyValidationMessage) &&
											validation.Count == 1;
			return validationCheckIsPassed;
		}

		public bool EmptyFormsValidationCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(TitleValidationMessage) &&
											validation.Contains(TaskBodyValidationMessage) && 
											validation.Count == 2;
			return validationCheckIsPassed;
		}

		public bool TitleMoreThan250ValidationCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(TitleMoreThan250ValidationMessage) &&
											validation.Count == 1;
			return validationCheckIsPassed;
		}

		public bool BodyMoreThan250ValidationCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(BodyMoreThan250ValidationMessage) &&
											validation.Count == 1;
			return validationCheckIsPassed;
		}

		public bool AllFieldsMoreThan250ValidationCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(BodyMoreThan250ValidationMessage) &&
											 validation.Contains(TitleMoreThan250ValidationMessage) &&
											 validation.Count == 2;
			return validationCheckIsPassed;
		}

		public bool BodyLessThan5ItemsCheck()
		{
			var validation = ValidationCheck();
			bool validationCheckIsPassed = validation.Contains(BodyLessThan5ItemsValidationMessage) &&
											validation.Count == 1;
			return validationCheckIsPassed;
		}

		public void DueDateDefaultValueReplace(string date)
		{
			string script = $"var event = new Event('change');" +
				$"var dueDate=document.getElementById('dueDate');" +
				$"dueDate.value='{date}';" +
				$"dueDate.dispatchEvent(event)";
			IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)Driver;
			javaScriptExecutor.ExecuteScript(script);
		}
		public string CurrentDate ()=> DateTime.Today.ToString("yyyy-MM-dd");

		public ViewTaskPage PopulateAllItemsAndSubmit (bool withDueDate=true)
        {
			if (withDueDate==true)
			{
				date = Randoms.GenerateRandomDateToString();
				DueDateDefaultValueReplace(date);
			}
			else
            {
				date = CurrentDate();
            }
			titleText = Randoms.GenerateStringValueInRange(1, 250);
			Title().SendKeys(titleText);
			bodyText = (Randoms.GenerateStringValueInRange(5, 250));
			TaskBody().SendKeys(bodyText);
			ClickSave();
			return new ViewTaskPage(Driver);
		}

		public bool EnsureAllItemsAreSavedCorrectly()
        {
			var viewTask = new ViewTaskPage(Driver);
			bool taskIsSavedCorrectly = viewTask.TaskTitleText() == titleText || viewTask.TaskItems()[1] == bodyText || viewTask.StringCreationDateValue() == CurrentDate() ||
							viewTask.StringDueDateValue() == date;
			return taskIsSavedCorrectly;
		}

		public void CheckFinishedCheckbox() => Driver.FindElement(By.Id("editPage")).Click();

		#endregion
	}
}
