using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	public class ViewTaskPage:Page
	{
		#region Constructors
		public ViewTaskPage(IWebDriver driver)
		{
			Driver = driver;
		}

		protected override string PageURL => "http://localhost:63508/taskview";
		#endregion
		#region Actions
		public void WaitForPageToBeLoaded()
		{
			WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(30));
			wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1[class=page-title]")));
		}

		public string TaskTitleText() => Driver.FindElement(By.CssSelector("h1[class=page-title]")).Text;

		public List<string> TaskItems()
		{
			var textItems = new List<string>();
			var elements=Driver.FindElements(By.CssSelector("label[class=form-control-plaintext]"));
			foreach(IWebElement item in elements)
			{
				textItems.Add(item.Text);
			}
			return textItems;
		}

		#endregion
	}
}
