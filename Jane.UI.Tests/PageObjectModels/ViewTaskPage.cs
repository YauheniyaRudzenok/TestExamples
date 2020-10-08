using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public class ViewTaskPage:Page
	{
		#region Constants

		private const string FinishedDefaultValue = "No";
		private const string FinishedCompletedValue = "Yes";

		#endregion

		#region Constructors

		public ViewTaskPage(IWebDriver driver)
		{
			Driver = driver;
		}

		protected override string PageURL => Configuration["appSettings:webURL"]+"/taskview";
		
		#endregion

		#region Elements
		public List<string> TaskItems()
		{
			var textItems = new List<string>();
			var elements = Driver.FindElements(By.CssSelector("label[class=form-control-plaintext]"));
			foreach (IWebElement item in elements)
			{
				textItems.Add(item.Text);
			}
			return elements.Select(i=>i.Text).ToList();
		}

		public IWebElement EditButton() => Driver.FindElement(By.XPath("//button[text()='Edit']"));

		#endregion

		#region Actions

		public void WaitForPageToBeLoaded()
		{
			WaitByCss("h1[class=page-title]");
		}

		public string TaskTitleText() => Driver.FindElement(By.CssSelector("h1[class=page-title]")).Text;

		public string StringDueDateValue() => DateTime.Parse(TaskItems()[3]).ToString("yyyy-MM-dd");

		public string StringCreationDateValue() => DateTime.Parse(TaskItems()[2]).ToString("yyyy-MM-dd");

		public void NavigateToViewPage(int id) => Driver.Navigate().GoToUrl(PageURL + "/" + id);

		public AddEditTaskPage ClickEditButton()
		{
			EditButton().Click();
			return new AddEditTaskPage(Driver);
		}

		#endregion

		#region Verification

		public bool CheckFinishedStatusIsCorrect(bool defaultStatus = true)
		{
			bool finishStatusIsCorrect;
			if (defaultStatus)
			{
				finishStatusIsCorrect = TaskItems()[0] == FinishedDefaultValue;
			}
			else
			{
				finishStatusIsCorrect = TaskItems()[0] == FinishedCompletedValue;

			}
			return finishStatusIsCorrect;
		}

		#endregion
	}
}
