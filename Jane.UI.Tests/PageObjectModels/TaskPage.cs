using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Jane.UI.Tests.PageObjectModels
{
	class TaskPage:Page
	{
		public TaskPage (IWebDriver driver)
		{
			Driver = driver;
		}

		protected override string PageURL => "http://localhost:63508/";
		protected override string PageTitle => "Jane.Todo.Web";

		public void WaitForPageLoaded()
		{
			WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(30));
			wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("h2")));
		}

		public List<string> TableHeader()
		{
			var headerList = Driver.FindElements(By.TagName("th"));
			var headers = new List<string>();
			foreach (IWebElement item in headerList)
			{
				headers.Add(item.Text);
			}
			return headers;
		}

		public AboutPage ClickAboutLink()
		{
			Driver.FindElement(By.CssSelector("a[href*='github']")).Click();
			return new AboutPage(Driver);
		}

		public LoginPage NavigateToLogin()
		{
			Driver.FindElement(By.CssSelector("a[href='/login'")).Click();
			return new LoginPage(Driver);
		}
	}
}
