using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public class AddTaskPage:Page
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
		#endregion
		#region Constructor
		public AddTaskPage(IWebDriver driver)
		{
			Driver = driver;
		}
		protected override string PageURL => "http://localhost:63508/taskedit";
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

		public bool emptyTitleValidationCheck()=> ValidationCheck().Contains(TitleValidationMessage) &&
							 ValidationCheck().Count == 1;

		public bool emptyBodyValidationCheck()=> ValidationCheck().Contains(TaskBodyValidationMessage) &&
							 ValidationCheck().Count == 1;

		public bool emptyFormsValidationCheck()=> ValidationCheck().Contains(TitleValidationMessage) &&
				ValidationCheck().Contains(TaskBodyValidationMessage) && ValidationCheck().Count == 2;

		public bool titleMoreThan250ValidationCheck()=> ValidationCheck().Contains(TitleMoreThan250ValidationMessage)&&
														ValidationCheck().Count == 1;


		public bool bodyMoreThan250ValidationCheck() => ValidationCheck().Contains(BodyMoreThan250ValidationMessage)&&
														ValidationCheck().Count == 1;

		public bool allFieldsMoreThan250ValidationCheck() => ValidationCheck().Contains(BodyMoreThan250ValidationMessage)&&
															ValidationCheck().Contains(TitleMoreThan250ValidationMessage)&&
															ValidationCheck().Count == 2;
		#endregion
	}
}
