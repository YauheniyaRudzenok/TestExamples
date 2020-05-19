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
		private const string TaskBodyValidationMessage= "The Note field is required.";
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

		public bool ValidationIsApplied()
		{
			bool validation = ValidationCheck().Contains(TitleValidationMessage) &&
							ValidationCheck().Contains(TaskBodyValidationMessage);
			return validation;
		}
			#endregion
	}
}
